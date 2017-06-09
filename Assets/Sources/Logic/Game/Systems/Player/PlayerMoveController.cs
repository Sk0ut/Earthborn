using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PlayerMoveController : ReactiveSystem<InputEntity>
{
    private readonly IGroup<GameEntity> _players;
    private readonly GameContext _game;

    public PlayerMoveController(Contexts contexts) : base(contexts.input)
    {
        _game = contexts.game;
        _players = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.Player,
            GameMatcher.Actor
        ));
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.Move);
    }

    protected override bool Filter(InputEntity entity)
    {
        return _game.turnState.value == TurnState.AskAction &&
               entity.hasMove;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var moveInput in entities)
        {
            foreach (var player in _players.GetEntities())
            {
                if (!player.Equals(_game.GetCurrentActor()) ||
                    player.hasMoveAction) continue;

                player.AddMoveAction(moveInput.move.value);
            }
        }
    }
}