using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PlayerAttackController : ReactiveSystem<InputEntity>
{
    private readonly IGroup<GameEntity> _players;
    private readonly GameContext _game;

    public PlayerAttackController(Contexts contexts) : base(contexts.input)
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
        return context.CreateCollector(InputMatcher.AttackCommand);
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.isAttackCommand;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var player in _players.GetEntities())
        {
            var action = _game.CreateAction(player);
            action.AddAttackAction(player.pointing.direction);;
        }
    }
}