using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class SwitchCameraSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;

    public SwitchCameraSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CurrentActor);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _game.hasCurrentActor &&
               _game.isCamera &&
               _game.GetCurrentActor().isPlayer &&
               _game.turnState.value == TurnState.AskAction;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var player = _game.GetCurrentActor();
        
        if (_game.cameraEntity.hasCameraFollow)
            _game.cameraEntity.RemoveCameraFollow();
        
        _game.cameraEntity.AddCameraFollow(player);
    }
}