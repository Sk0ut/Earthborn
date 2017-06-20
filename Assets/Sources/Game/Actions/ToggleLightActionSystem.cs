using System.Collections.Generic;
using System.Linq;
using Entitas;

public class ToggleLightActionSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameContext;
    private readonly IGroup<GameEntity> _lightSourceGroup;

    public ToggleLightActionSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
        _lightSourceGroup = _gameContext.GetGroup(GameMatcher.AllOf(
            GameMatcher.LightSource,
            GameMatcher.AttachedTo, GameMatcher.View
        ));
    }

    #region implemented abstract members of ReactiveSystem

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Action, GameMatcher.ToggleLightAction));
    }

    protected override bool Filter(GameEntity action)
    {
        return action.isAction && action.isToggleLightAction;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var currentActor = _gameContext.GetCurrentActor();
            var light = _lightSourceGroup.GetEntities().First(e => e.attachedTo.value.Equals(currentActor));

            if (!light.lightSource.active && currentActor.oil.value <= 0) continue;
            
            light.lightSource.active = !light.lightSource.active;
            light.ReplaceComponent(GameComponentsLookup.LightSource, light.lightSource);
            currentActor.ReplaceActorEnergy(currentActor.actorEnergy.energy - 1f);
            _gameContext.ReplaceTurnState(TurnState.EndTurn);

            entity.Destroy();
            
            var evt = _gameContext.CreateEvent(Event.ActorToggleLight);
            evt.AddTarget(currentActor);
        }
    }

    #endregion
}