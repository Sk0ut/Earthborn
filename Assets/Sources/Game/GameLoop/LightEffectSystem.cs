using Entitas;

public class LightEffectSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    private readonly IGroup<GameEntity> _lightSourceGroup;

    public LightEffectSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
        _lightSourceGroup = _gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.LightSource, GameMatcher.AttachedTo));
    }

    #region implemented abstract members of ReactiveSystem

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.TurnState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.turnState.value == TurnState.AskAction
        && _gameContext.GetCurrentActor().isPlayer
        && _gameContext.GetCurrentActor().hasHealth;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities)
    {
        var player = _gameContext.GetCurrentActor();
        player.health.value += iluminated(player) ?
			_gameContext.globals.value.HealthIncreasePerTurn
			: -_gameContext.globals.value.HealthDecreasePerTurn;
        if (player.health.value > player.health.max)
        {
            player.health.value = player.health.max;
        }
        else if (player.health.value < 0)
        {
            player.health.value = 0;
        }
        player.ReplaceComponent(GameComponentsLookup.Health, player.health);
    }

    private bool iluminated(GameEntity entity)
    {
        foreach (var e in _lightSourceGroup.GetEntities())
        {
            if (e.attachedTo.value == entity && e.lightSource.active)
            {
                return true;
            }
        }
        return false;
    }

    #endregion
}

