using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class EndTurnSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _context;

    public EndTurnSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.TurnState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _context.turnState.value == TurnState.EndTurn;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var current = _context.currentActor.value;
        
        _context.ReplaceCurrentActor(current.Next ?? current.List.First);
        Debug.Log("Actor " + current.Value + " has finished his turn");
    }
}