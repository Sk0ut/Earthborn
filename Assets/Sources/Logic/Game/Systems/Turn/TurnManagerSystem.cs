using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class TurnManagerSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _context;

    public TurnManagerSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CurrentActor.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (!_context.hasCurrentActor)
        {
            _context.ReplaceTurnState(TurnState.Waiting);
            return;
        }

        var currentActor = _context.GetCurrentActor();
        _context.ReplaceTurnState(TurnState.StartTurn);
    }
}