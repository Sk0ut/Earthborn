using System;
using Entitas;
using Entitas.Unity;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

class MapSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
	private readonly GameContext _gameContext;
	private readonly IGroup<GameEntity> _removableEntitiesGroup;
	private readonly IGroup<GameEntity> _playerGroup;
	private readonly IGroup<GameEntity> _groundTiles;
	private readonly IGroup<GameEntity> _blockingEntities;
	private int lastFloor;
	private System.Random rnd;
	protected Floor [] floors;

	public MapSystem(Contexts contexts) : base(contexts.game)
    {
		rnd = new System.Random ();
        _gameContext = contexts.game;
		_removableEntitiesGroup = _gameContext.GetGroup (GameMatcher.AllOf(GameMatcher.Position).NoneOf(GameMatcher.Player));
		_playerGroup = _gameContext.GetGroup (GameMatcher.Player);
		_groundTiles = _gameContext.GetGroup (GameMatcher.AllOf (GameMatcher.Position, GameMatcher.Tile).NoneOf (GameMatcher.Blocking));
		_blockingEntities = _gameContext.GetGroup (GameMatcher.AllOf (GameMatcher.Position, GameMatcher.Blocking).NoneOf (GameMatcher.Tile));
		floors = Maps.floors;
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

	protected virtual void createFloor(int currentFloor) {
		Floor floor = floors [currentFloor];
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

		/* Disable transition to lower floors
		if (currentFloor > 0) {
			var entity = _gameContext.CreateEntity ();
			entity.AddPosition ((int)floor.start.x, (int)floor.start.y);
			entity.AddFloorTransition (currentFloor - 1);
			entity.AddAsset (_gameContext.assets.value.Stairs);
		}
		*/
		if (currentFloor < floors.Length - 1) {
			var entity = _gameContext.CreateEntity ();
			entity.AddPosition ((int)floor.end.x, (int)floor.end.y);
			entity.AddFloorTransition (currentFloor + 1);
			entity.AddAsset (_gameContext.assets.value.Stairs);
		}

		if (currentFloor == floors.Length - 1) {
			_gameContext.CreateBoss (new UnityEngine.Vector2 ((int)floor.end.x, (int)floor.end.y - 1));
		} else {
			var pistol = _gameContext.CreateEntity();
			pistol.isItem = true;
			pistol.AddItemId(ItemId.BasePistol);
			pistol.AddItemStats("X375-R", "Most powerful pistol in all of Earth", 100);
			pistol.isEquipableItem = true;
			pistol.isGun = true;
			pistol.AddAsset(_gameContext.assets.value.Pistol);
			pistol.isSeethrough = true;

			var pistol2 = _gameContext.CreateEntity();
			pistol2.isItem = true;
			pistol2.AddItemId(ItemId.BasePistol);
			pistol2.AddItemStats("X375-X", "Most powerful pistol in all of Earth 2", 120);
			pistol2.isEquipableItem = true;
			pistol2.isGun = true;
			pistol2.AddAsset(_gameContext.assets.value.Pistol);
			pistol2.isSeethrough = true;

			var hpPot = _gameContext.CreateEntity();
			hpPot.isItem = true;
			hpPot.AddItemId(ItemId.HealthPotion);
			hpPot.AddItemStats("Sanity Potion +25", "Lonely people need lonely solutions", 20);
			hpPot.isUsableItem = true;
			hpPot.AddAsset(_gameContext.assets.value.HealthPotion);
			hpPot.isSeethrough = true;

			var oilPot = _gameContext.CreateEntity();
			oilPot.isItem = true;
			oilPot.AddItemId(ItemId.HealthPotion);
			oilPot.AddItemStats("Oil Potion +25", "Lonely people need lonely solutions", 20);
			oilPot.isUsableItem = true;
			oilPot.AddAsset(_gameContext.assets.value.OilPotion);
			oilPot.isSeethrough = true;

			List < GameEntity> entities = new List<GameEntity> ();

			int numMonsters = currentFloor * 3 + 2;
			for (int i = 0; i < numMonsters; ++i) {
				entities.Add(_gameContext.CreateEnemy (Vector2.zero));
			}
			entities.Add (pistol);
			entities.Add (pistol2);
			entities.Add (oilPot);
			entities.Add (hpPot);


			foreach (var entity in entities) {
				GameEntity emptyTile;
				do {
					emptyTile = _groundTiles.GetEntities () [rnd.Next (_groundTiles.GetEntities ().Length)];
				} while (_blockingEntities.GetEntities ().Any ((e) => e.position.x == emptyTile.position.x && e.position.y == emptyTile.position.y));
				entity.ReplacePosition(emptyTile.position.x, emptyTile.position.y);
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