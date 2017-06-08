using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class TurnManagerSystem : ReactiveSystem<GameEntity>
{
    private GameContext _context;

    public TurnManagerSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CurrentActor);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var current = _context.currentActor.value;
        var currentActor = current.Value;
        if (currentActor != null)
        {
            currentActor.ReplaceActorState(ActorState.TURN);
            Debug.Log("New turn: " + currentActor);
        }
        else
        {
            _context.ReplaceCurrentActor(current.Next ?? current.List.First);
        }
    }
}