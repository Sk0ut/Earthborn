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
            GameMatcher.Actor,
            GameMatcher.Pointing
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
        var moveInput = entities[0];
        foreach (var player in _players.GetEntities())
        {
            var action = _game.CreateAction(player);
            if (moveInput.move.value == player.pointing.direction)
            {
                action.AddMoveAction(moveInput.move.value);
            }
            else if (_game.turnState.value == TurnState.AskAction)
            {
                player.ReplacePointing(moveInput.move.value);
            }
        }
    }
}