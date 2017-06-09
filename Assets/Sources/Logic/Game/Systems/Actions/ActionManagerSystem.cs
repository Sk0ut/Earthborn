using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ActionManagerSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _context;
    public ActionManagerSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(
            GameMatcher.Action.Added(),
            GameMatcher.Target.Added()
        );
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isAction &&
               entity.hasTarget;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var actions in entities)
        {
            // Only allow actions target at the current actor and
            // in AskAction state
            if (!actions.target.value.Equals(_context.GetCurrentActor()) ||
                _context.turnState.value != TurnState.AskAction)
                actions.Destroy();
        }
    }
}