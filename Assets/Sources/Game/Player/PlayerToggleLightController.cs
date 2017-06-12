using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class PlayerToggleLightController : ReactiveSystem<InputEntity>
{
    private readonly IGroup<GameEntity> _players;
    private readonly GameContext _game;

    public PlayerToggleLightController(Contexts contexts) : base(contexts.input)
    {
        _game = contexts.game;
        _players = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.Player,
            GameMatcher.Actor
        ));
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.AllOf(
            InputMatcher.Command,
            InputMatcher.ToggleLightCommand
        ));
    }

    protected override bool Filter(InputEntity command)
    {
        return command.isCommand && command.isToggleLightCommand;
    }

    protected override void Execute(List<InputEntity> commands)
    {    
        foreach (var player in _players.GetEntities())
        {
            var action = _game.CreateAction(player);
            action.isToggleLightAction = true;
        }
    }
}