using Entitas;

public class ToggleLightSystem : ReactiveSystem<InputEntity>
{
	private readonly GameContext _gameContext;
	private readonly IGroup<GameEntity> _lightSourceGroup;

	public ToggleLightSystem (Contexts contexts) : base(contexts.input)
	{
		_gameContext = contexts.game;
		_lightSourceGroup = _gameContext.GetGroup (GameMatcher.AllOf(GameMatcher.LightSource, GameMatcher.AttachedTo, GameMatcher.View));
	}

	#region implemented abstract members of ReactiveSystem

	protected override ICollector<InputEntity> GetTrigger (IContext<InputEntity> context)
	{
		return context.CreateCollector (InputMatcher.AllOf(InputMatcher.Command, InputMatcher.ToggleLightCommand));
	}

	protected override bool Filter (InputEntity entity)
	{
		return entity.isCommand && entity.isToggleLightCommand;
	}

	protected override void Execute (System.Collections.Generic.List<InputEntity> entities)
	{
		foreach (var entity in entities) {
			var currentActor = _gameContext.GetCurrentActor ();
			foreach (var lightEntity in _lightSourceGroup.GetEntities()) {
				if (lightEntity.attachedTo.value == currentActor) {
					lightEntity.lightSource.active = !lightEntity.lightSource.active;
					lightEntity.ReplaceComponent(GameComponentsLookup.LightSource, lightEntity.lightSource);
					currentActor.ReplaceActorEnergy (currentActor.actorEnergy.energy - 1f);
					_gameContext.ReplaceTurnState (TurnState.EndTurn);

					break;
				}
			}
			entity.Destroy ();
		}
	}

	#endregion
}

