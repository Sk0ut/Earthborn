using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PlayerWaitController : ReactiveSystem<InputEntity>
{
    private readonly IGroup<GameEntity> _players;
    private readonly GameContext _game;

    public PlayerWaitController(Contexts contexts) : base(contexts.input)
    {
        _game = contexts.game;
        _players = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.Player,
            GameMatcher.Actor
        ));
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.WaitCommand);
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.isWaitCommand;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var player in _players.GetEntities())
        {
            var action = _game.CreateAction(player);
            action.isWaitAction = true;
        }
    }
}