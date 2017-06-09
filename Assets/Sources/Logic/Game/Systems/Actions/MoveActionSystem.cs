using System.Collections.Generic;
using Entitas;

public class MoveActionSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _context;
    public MoveActionSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.MoveAction);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _context.turnState.value == TurnState.AskAction &&
               entity.hasMoveAction &&
               entity.isActor &&
               entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var actorEntity in entities)
        {
            var pos = actorEntity.position;

            switch (actorEntity.moveAction.value)
            {
                case MoveDirection.Up:
                    actorEntity.ReplacePosition(pos.x, pos.y + 1);
                    break;
                case MoveDirection.Down:
                    actorEntity.ReplacePosition(pos.x, pos.y - 1);
                    break;
                case MoveDirection.Left:
                    actorEntity.ReplacePosition(pos.x - 1, pos.y);
                    break;
                case MoveDirection.Right:
                    actorEntity.ReplacePosition(pos.x + 1, pos.y);
                    break;
            }

            actorEntity.ReplaceActorEnergy(actorEntity.actorEnergy.energy - 1f);
            _context.ReplaceTurnState(TurnState.EndTurn);
            actorEntity.RemoveMoveAction();
        }
    }
}