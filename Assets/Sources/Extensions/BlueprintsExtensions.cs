using UnityEngine;

public static class BlueprintsExtensions
{
	public static GameEntity GetCurrentActor(this GameContext context)
	{
		return context.currentActor.value.Value;
	}
	
    public static GameEntity CreateTile(this GameContext context, Vector2 position, TileType type) {
        var entity = context.CreateEntity();
        entity.AddPosition((int)position.x, (int)position.y);
        entity.AddTile(type);

        return entity;
    }

	public static GameEntity CreatePlayer(this GameContext context, Vector2 position) {
		var entity = context.CreateEntity ();
		entity.isPlayer = true;
		entity.AddHealth(context.globals.value.PlayerHealth);
		entity.AddAsset(context.assets.value.Player);
		entity.AddPosition((int)position.x, (int)position.y);
		entity.AddEasing(0.5f);
		entity.MakeActor(0.5f, 0f);

		return entity;
	}
	
	public static GameEntity CreateActor(this GameContext context, float speed = 0.5f, float energy = 0f) {
		return context.CreateEntity().MakeActor(speed, energy);
	}
	
	public static GameEntity MakeActor(this GameEntity entity, float speed = 0.5f, float energy = 0f) {
		entity.isActor = true;
		if (energy >= 0) entity.AddActorEnergy(energy);
		if (speed > 0) entity.AddActorSpeed(speed);

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