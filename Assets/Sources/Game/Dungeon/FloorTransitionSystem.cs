using Entitas;

public class FloorTransitionSystem : ReactiveSystem<GameEntity>
{
	private GameContext _gameContext;
	private IGroup<GameEntity> _floorTransitionsGroup;

	public FloorTransitionSystem (Contexts contexts) : base(contexts.game)
	{
		_gameContext = contexts.game;
		_floorTransitionsGroup = _gameContext.GetGroup (GameMatcher.AllOf (GameMatcher.FloorTransition, GameMatcher.Position));
	}

	#region implemented abstract members of ReactiveSystem

	protected override ICollector<GameEntity> GetTrigger (IContext<GameEntity> context)
	{
		return context.CreateCollector (GameMatcher.AllOf (GameMatcher.Player, GameMatcher.Position));
	}

	protected override bool Filter (GameEntity entity)
	{
		return entity.isPlayer && entity.hasPosition;
	}

	protected override void Execute (System.Collections.Generic.List<GameEntity> entities)
	{
		var player = entities.SingleEntity ();

		foreach (var e in _floorTransitionsGroup.GetEntities()) {
			if (e.position.x == player.position.x && e.position.y == player.position.y) {
				_gameContext.ReplaceCurrentFloor (e.floorTransition.destinationFloor);
				break;
			}
		}
	}

	#endregion
}

