using UnityEngine;

public static class GameExtensions
{
	// @TODO there must be a better way to do this
	public static GameEntity GetCurrentActor(this GameContext context)
	{
		return context.currentActor.value.Value;
	}
	
	public static GameEntity CreateTile(this GameContext context, Vector2 position, TileType type, bool blocking, GameObject asset) {
        var entity = context.CreateEntity();
        entity.AddPosition((int)position.x, (int)position.y);
        entity.AddTile(type);
		if (type == TileType.WALL)
		{
			entity.isObstructable = true;
			
			var ground = context.CreateEntity();
			ground.AddPosition((int)position.x, (int)position.y);
			ground.AddAsset(context.assets.value.GroundTile);
			ground.isVisible = true;
		}
		entity.isBlocking = blocking;
		entity.AddAsset (asset);
		entity.isVisible = true;
		entity.isCollider = blocking;

        return entity;
    }

	public static GameEntity CreatePlayer(this GameContext context, Vector2 position) {
		var player = context.CreateActor (0.5f, 0f);
		player.isPlayer = true;
		player.AddHealth(context.globals.value.PlayerHealth, context.globals.value.PlayerHealth);
		player.AddAsset(context.assets.value.Player);
		player.isVisible = true;
		player.AddPosition((int)position.x, (int)position.y);
		player.AddPointing(Direction.Up);

		player.AddEasing(0.5f);
		player.isSeethrough = true;
		player.AddDamage(context.globals.value.PlayerDamage);
		player.AddUnitType(Unit.Adventurer);

		var inventory = context.CreateEntity();
		inventory.isInventory = true;
		inventory.isStorage = true;
		inventory.AddStorageCapacity(10);
		inventory.AddInventoryOwner(player);

		return player;
	}

	public static GameEntity CreateEnemy(this GameContext context, Vector2 position) {
		var enemy = context.CreateActor ();
		enemy.AddAIControl (AIControlType.RANDOM);
		enemy.AddAsset (context.assets.value.Enemy);
		enemy.AddPosition ((int)position.x, (int)position.y);
		enemy.AddPointing (Direction.Up);
		enemy.AddEasing (0.5f);
		enemy.AddHealth (context.globals.value.EnemyHealth, context.globals.value.EnemyHealth);
		enemy.AddDamage (context.globals.value.EnemyDamage);
		enemy.AddUnitType(Unit.Minion);

		return enemy;
	}

	public static GameEntity CreateBoss(this GameContext context, Vector2 position) {
		var enemy = context.CreateActor ();
		enemy.AddAIControl (AIControlType.RANDOM);
		enemy.AddAsset (context.assets.value.Boss);
		enemy.AddPosition ((int)position.x, (int)position.y);
		enemy.AddPointing (Direction.Up);
		enemy.AddEasing (0.5f);
		enemy.isBoss = true;
		enemy.AddUnitType(Unit.MinionBoss);

		enemy.AddHealth (context.globals.value.EnemyHealth * context.globals.value.BossMultiplier, context.globals.value.EnemyHealth * context.globals.value.BossMultiplier);
		enemy.AddDamage (context.globals.value.EnemyDamage * context.globals.value.BossMultiplier);

		return enemy;
	}

	public static GameEntity CreateActor(this GameContext context, float speed = 0.5f, float energy = 0f) {
		var entity = context.CreateEntity();
		entity.isActor = true;
		if (energy >= 0) entity.AddActorEnergy(energy);
		if (speed > 0) entity.AddActorSpeed(speed);
		entity.isCollider = true;
		entity.isBlocking = true;

		return entity;
	}

	public static GameEntity CreateLight(this GameContext context, bool active, int radius, GameEntity attached) {
		var entity = context.CreateEntity ();
		entity.AddLightSource (active, radius);
		entity.AddAttachedTo (attached);
		entity.AddAsset (context.assets.value.LightSource);
		entity.isVisible = true;

		return entity;
	}
	
	public static GameEntity CreateAction(this GameContext context, GameEntity target)
	{
		var action = context.CreateEntity();
		action.isAction = true;
		action.AddTarget(target);
		return action;
	}
	
	public static GameEntity CreateEvent(this GameContext context, Event eventType)
	{
		var ev = context.CreateEntity();
		ev.AddEventType(eventType);
		return ev;
	}

	public static int GetDirectionAngle(Direction direction)
	{
		var directionAngle = 0;
		switch (direction)
		{
			case Direction.Left:
				directionAngle = -90;
				break;
			case Direction.Down:
				directionAngle = 180;
				break;
			case Direction.Right:
				directionAngle = 90;
				break;
		}

		return directionAngle;
	}
}