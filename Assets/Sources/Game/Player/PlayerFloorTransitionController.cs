using Entitas;

public class PlayerFloorTransitionController : ReactiveSystem<InputEntity>
{
	private readonly GameContext _gameContext;
	private readonly IGroup<GameEntity> _playerGroup;
	private readonly IGroup<GameEntity> _floorTransitionGroup;

	public PlayerFloorTransitionController (Contexts contexts) : base(contexts.input)
	{
		_gameContext = contexts.game;
		_playerGroup = _gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Position));
		_floorTransitionGroup = _gameContext.GetGroup (GameMatcher.AllOf (GameMatcher.FloorTransition, GameMatcher.Position));
	}

	#region implemented abstract members of ReactiveSystem

	protected override ICollector<InputEntity> GetTrigger (IContext<InputEntity> context)
	{
		return context.CreateCollector(InputMatcher.AllOf(InputMatcher.Command, InputMatcher.FloorTransictionCommand));
	}

	protected override bool Filter (InputEntity entity)
	{
		return entity.isCommand && entity.isFloorTransictionCommand;
	}

	protected override void Execute (System.Collections.Generic.List<InputEntity> entities)
	{
		var player = _playerGroup.GetSingleEntity ();
		foreach (var e in _floorTransitionGroup.GetEntities()) {
			if (player.position.x == e.position.x && player.position.y == e.position.y) {
				var action = _gameContext.CreateAction (player);
				action.AddFloorTransitionAction(e.floorTransition.destinationFloor);
				break;
			}
		}
	}

	#endregion
}
