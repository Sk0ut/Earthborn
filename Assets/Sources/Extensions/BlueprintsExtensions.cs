using UnityEngine;

public static class BlueprintsExtensions
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
		}
		entity.isBlocking = blocking;
		entity.AddAsset (asset);
		entity.isCollider = blocking;

        return entity;
    }

	public static GameEntity CreatePlayer(this GameContext context, Vector2 position) {
		var entity = context.CreateActor (0.5f, 0f);
		entity.isPlayer = true;
		entity.AddHealth(context.globals.value.PlayerHealth);
		entity.AddMaxHealth (context.globals.value.PlayerHealth);
		entity.AddAsset(context.assets.value.Player);
		entity.AddPosition((int)position.x, (int)position.y);

		entity.AddEasing(0.5f);
		entity.isAlwaysVisible = true;

		return entity;
	}
	
	public static GameEntity CreateActor(this GameContext context, float speed = 0.5f, float energy = 0f) {
		var entity = context.CreateEntity ();
		entity.isActor = true;
		if (energy >= 0) entity.AddActorEnergy(energy);
		if (speed > 0) entity.AddActorSpeed(speed);
		entity.isCollider = true;

		return entity;
	}

	public static GameEntity CreateLight(this GameContext context, bool active, int radius, GameEntity attached) {
		var entity = context.CreateEntity ();
		entity.AddLightSource (active, radius);
		entity.AddAttachedTo (attached);
		entity.AddAsset (context.assets.value.LightSource);

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
	
	
	// Input
	public static InputEntity CreateInput(this InputContext context)
	{
		var input = context.CreateEntity();
		input.isInput = true;
		
		return input;
	}
	
	public static InputEntity CreateCommand(this InputContext context)
	{
		var command = context.CreateEntity();
		command.isCommand = true;
		
		return command;
	}
}