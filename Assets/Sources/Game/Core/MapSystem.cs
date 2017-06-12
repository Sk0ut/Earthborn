using System;
using Entitas;
using Entitas.Unity;
using UnityEngine;
using System.Linq;

class MapSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
	private readonly GameContext _gameContext;
	private readonly IGroup<GameEntity> _removableEntitiesGroup;
	private readonly IGroup<GameEntity> _playerGroup;
	private readonly IGroup<GameEntity> _groundTiles;
	private readonly IGroup<GameEntity> _blockingEntities;
	private int lastFloor;
	private System.Random rnd;

	public MapSystem(Contexts contexts) : base(contexts.game)
    {
		rnd = new System.Random ();
        _gameContext = contexts.game;
		_removableEntitiesGroup = _gameContext.GetGroup (GameMatcher.AllOf(GameMatcher.Position).NoneOf(GameMatcher.Player));
		_playerGroup = _gameContext.GetGroup (GameMatcher.Player);
		_groundTiles = _gameContext.GetGroup (GameMatcher.AllOf (GameMatcher.Position, GameMatcher.Tile).NoneOf (GameMatcher.Blocking));
		_blockingEntities = _gameContext.GetGroup (GameMatcher.AllOf (GameMatcher.Position, GameMatcher.Blocking).NoneOf (GameMatcher.Tile));
    }


    public void Initialize()
    {
		lastFloor = -1;
		_gameContext.SetCurrentFloor (0);
		createFloor (_gameContext.currentFloor.value);
    }

	#region implemented abstract members of ReactiveSystem

	protected override ICollector<GameEntity> GetTrigger (IContext<GameEntity> context)
	{
		return context.CreateCollector (GameMatcher.CurrentFloor);
	}

	protected override bool Filter (GameEntity entity)
	{
		return entity.hasCurrentFloor && entity.currentFloor.value != lastFloor;
	}

	protected override void Execute (System.Collections.Generic.List<GameEntity> entities)
	{

		destroyFloor ();
		createFloor (entities.SingleEntity().currentFloor.value);
	}

	#endregion

	private void createFloor(int currentFloor) {
		Floor floor = Maps.floors [currentFloor];
		int[,] map = floor.tiles;

		for (var y = 0; y < map.GetLength(0); ++y)
		{
			for (var x = 0; x < map.GetLength(1); ++x)
			{
				TileType type;
				UnityEngine.GameObject asset;
				bool blocking;
				switch (map[y, x]) {
				case 0:
					type = TileType.GROUND;
					asset = _gameContext.assets.value.GroundTile;
					blocking = false;
					break;
				case 1:
				default:
					type = TileType.WALL;
					asset = _gameContext.assets.value.Wall;
					blocking = true;
					break;
				}
				_gameContext.CreateTile (new UnityEngine.Vector2 (x, map.GetLength (0) - y - 1), type, blocking, asset);
			}
		}
		var player = _playerGroup.GetSingleEntity ();
		if (lastFloor < currentFloor) {
			player.ReplacePosition ((int)floor.start.x, (int)floor.start.y);
		} else {
			player.ReplacePosition ((int)floor.end.x, (int)floor.end.y);
		}

		if (player.hasView) {
			player.ReplaceComponent (GameComponentsLookup.View, player.view);
		}

		if (currentFloor > 0) {
			var entity = _gameContext.CreateEntity ();
			entity.AddPosition ((int)floor.start.x, (int)floor.start.y);
			entity.AddFloorTransition (currentFloor - 1);
		}
		if (currentFloor < Maps.floors.Length - 1) {
			var entity = _gameContext.CreateEntity ();
			entity.AddPosition ((int)floor.end.x, (int)floor.end.y);
			entity.AddFloorTransition (currentFloor + 1);
		}

		if (currentFloor == Maps.floors.Length - 1) {
			_gameContext.CreateBoss (new UnityEngine.Vector2 ((int)floor.end.x, (int)floor.end.y - 1));
		} else {
			int numMonsters = currentFloor * 3 + 2;
			for (int i = 0; i < numMonsters; ++i) {
				GameEntity freeTile;
				do {
					freeTile = _groundTiles.GetEntities()[rnd.Next(_groundTiles.GetEntities().Length)];
				} while (_blockingEntities.GetEntities().Any((e) => e.position.x == freeTile.position.x && e.position.y == freeTile.position.y));

				_gameContext.CreateEnemy(new Vector2(freeTile.position.x, freeTile.position.y));
			}
		}

		lastFloor = currentFloor;
	}

	private void destroyFloor() {
		foreach (var entity in _removableEntitiesGroup.GetEntities()) {
			if (entity.hasView) {
				entity.view.gameObject.Unlink ();
				UnityEngine.Object.Destroy (entity.view.gameObject);
			}
			entity.Destroy ();
		}	
	}
}