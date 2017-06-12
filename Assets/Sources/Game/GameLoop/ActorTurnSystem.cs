using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ActorTurnSystem : ReactiveSystem<GameEntity>
{
    private readonly float _threshold;
    private readonly GameContext _context;

    public ActorTurnSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
        _threshold = contexts.game.globals.value.EnergyThreshold;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.TurnState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _context.turnState.value == TurnState.StartTurn;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var entity = _context.GetCurrentActor();

        if (entity.hasActorEnergy && entity.actorEnergy.energy < _threshold)
        {
            _context.ReplaceTurnState(TurnState.EndTurn);
            return;
        }

        _context.ReplaceTurnState(TurnState.AskAction);
    }
}