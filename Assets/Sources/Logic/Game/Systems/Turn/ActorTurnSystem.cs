using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ActorTurnSystem : ReactiveSystem<GameEntity>
{
    private readonly float _threshold;
    
    public ActorTurnSystem(Contexts contexts) : base(contexts.game)
    {
        _threshold = contexts.game.globals.value.EnergyThreshold;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ActorState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isActor
               && entity.actorState.value == ActorState.TURN;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.hasActorEnergy && entity.actorEnergy.energy < _threshold)
            {
                entity.ReplaceActorState(ActorState.ACTED);
                continue;
            }
            
            Debug.Log("Actor " + entity + " can now act");
            entity.ReplaceActorState(ActorState.ACTING);
        }
    }
}