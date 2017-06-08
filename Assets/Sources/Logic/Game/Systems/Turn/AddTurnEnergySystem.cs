using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AddTurnEnergySystem : ReactiveSystem<GameEntity>
{
    public AddTurnEnergySystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ActorState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isActor
               && entity.hasActorEnergy
               && entity.actorState.value == ActorState.TURN;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var speed = entity.hasActorSpeed ? entity.actorSpeed.speed : 0.5f;
            entity.ReplaceActorEnergy(entity.actorEnergy.energy += speed);
            Debug.Log("Actor " + entity + " has gained " + speed + " energy");
        }
    }
}