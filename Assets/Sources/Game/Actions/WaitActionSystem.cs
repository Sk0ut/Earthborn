using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class WaitActionSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _context;

    public WaitActionSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(
            GameMatcher.WaitAction,
            GameMatcher.Target
        ));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isWaitAction &&
               entity.hasTarget;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var action in entities)
        {
            var target = action.target.value;

            target.ReplaceActorEnergy(target.actorEnergy.energy - 1f);
            _context.ReplaceTurnState(TurnState.EndTurn);
            _context.CreateEvent(Event.ActorWaited)
                .AddTarget(target);
            // Handled
            action.Destroy();
        }
    }
}