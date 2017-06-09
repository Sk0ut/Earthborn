using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class MoveActionSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _context;
    public MoveActionSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(
            GameMatcher.MoveAction,
            GameMatcher.Target
        ));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasMoveAction &&
               entity.hasTarget &&
               entity.target.value.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var action in entities)
        {
            var target = action.target.value;
            
            var pos = target.position;

            switch (action.moveAction.value)
            {
                case MoveDirection.Up:
                    target.ReplacePosition(pos.x, pos.y + 1);
                    break;
                case MoveDirection.Down:
                    target.ReplacePosition(pos.x, pos.y - 1);
                    break;
                case MoveDirection.Left:
                    target.ReplacePosition(pos.x - 1, pos.y);
                    break;
                case MoveDirection.Right:
                    target.ReplacePosition(pos.x + 1, pos.y);
                    break;
            }

            target.ReplaceActorEnergy(target.actorEnergy.energy - 1f);
            _context.ReplaceTurnState(TurnState.EndTurn);
            // Handled
            action.Destroy();
        }
    }
}