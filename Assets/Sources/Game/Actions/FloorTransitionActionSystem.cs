using Entitas;

public class FloorTransitionActionSystem : ReactiveSystem<GameEntity>
{
	GameContext _gameContext;

	public FloorTransitionActionSystem (Contexts contexts) : base(contexts.game)
	{
		_gameContext = contexts.game;
	}

	#region implemented abstract members of ReactiveSystem

	protected override ICollector<GameEntity> GetTrigger (IContext<GameEntity> context)
	{
		return context.CreateCollector (GameMatcher.AllOf (GameMatcher.Action, GameMatcher.FloorTransitionAction));
	}

	protected override bool Filter (GameEntity entity)
	{
		return entity.isAction && entity.hasFloorTransitionAction;
	}

	protected override void Execute (System.Collections.Generic.List<GameEntity> entities)
	{
		var action = entities.SingleEntity ();
		_gameContext.ReplaceCurrentFloor(action.floorTransitionAction.destinationFloor);
	}

	#endregion
}

