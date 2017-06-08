using System.Collections.Generic;
using Entitas;

public class PlayerMoveController : ReactiveSystem<InputEntity>
{
    private IGroup<GameEntity> _players;

    public PlayerMoveController(Contexts contexts) : base(contexts.input)
    {
        _players = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.Player,
            GameMatcher.Actor,
            GameMatcher.ActorState
        ));
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Move);
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasMove;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var moveInput in entities)
        {
            foreach (var player in _players.GetEntities())
            {
                if (player.hasMoveAction ||
                    player.actorState.value != ActorState.ACTING) continue;

                player.AddMoveAction(moveInput.move.value);
            }
        }
    }
}