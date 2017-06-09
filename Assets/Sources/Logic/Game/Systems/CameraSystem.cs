using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class CameraSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;
    
    public CameraSystem(Contexts contexts) : base(contexts.game)
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
               _game.cameraEntity.hasView &&
               _game.GetCurrentActor().isPlayer &&
               _game.turnState.value == TurnState.AskAction;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var player = _game.GetCurrentActor();

        _game.cameraEntity.view.gameObject.transform.DOMove(
            new Vector3(player.position.x, 3f, player.position.y - 3),
            1f
        );
    }
}