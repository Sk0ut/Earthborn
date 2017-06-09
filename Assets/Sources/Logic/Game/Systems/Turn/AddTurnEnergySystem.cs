using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AddTurnEnergySystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _context;

    public AddTurnEnergySystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.TurnState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _context.turnState.value == TurnState.StartTurn
            && _context.GetCurrentActor().hasActorEnergy;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var entity = _context.GetCurrentActor();

        var speed = entity.hasActorSpeed ? entity.actorSpeed.speed : 0.5f;
        entity.ReplaceActorEnergy(entity.actorEnergy.energy + speed);
        Debug.Log("Actor " + entity + " has gained " + speed + " energy");
    }
}