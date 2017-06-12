using System;
using Entitas;
using Entitas.Unity;
using UnityEngine;

class MapSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
	private readonly GameContext _gameContext;
	private readonly IGroup<GameEntity> _removableEntitiesGroup;
	private readonly IGroup<GameEntity> _playerGroup;

	public MapSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
		_removableEntitiesGroup = _gameContext.GetGroup (GameMatcher.AllOf(GameMatcher.Position).NoneOf(GameMatcher.Player));
		_playerGroup = _gameContext.GetGroup (GameMatcher.Player);
    }


    public void Initialize()
    {
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
		return entity.hasCurrentFloor;
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
		player.ReplacePosition ((int) floor.start.x, (int)floor.start.y);
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