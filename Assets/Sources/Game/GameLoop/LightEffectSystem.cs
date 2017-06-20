using System;
using System.Linq;
using Entitas;
using UnityEngine;

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
        return entity.turnState.value == TurnState.AskAction &&
               _gameContext.GetCurrentActor().isPlayer &&
               _gameContext.GetCurrentActor().hasHealth &&
               _gameContext.GetCurrentActor().hasOil;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities)
    {
        var player = _gameContext.GetCurrentActor();
        player.health.value += iluminated(player)
            ? _gameContext.globals.value.HealthIncreasePerTurn
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

        if (iluminated(player))
        {
            player.oil.value = Math.Max(0, player.oil.value -= 1);
            player.ReplaceComponent(GameComponentsLookup.Oil, player.oil);

            var light = _lightSourceGroup.GetEntities().First(e => e.attachedTo.value.Equals(player));
            if (player.oil.value <= 0 && light.lightSource.active)
            {
                light.lightSource.active = false;
                light.ReplaceComponent(GameComponentsLookup.LightSource, light.lightSource);
            
                var evt = _gameContext.CreateEvent(Event.ActorToggleLight);
                evt.AddTarget(player);
            }
        }
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