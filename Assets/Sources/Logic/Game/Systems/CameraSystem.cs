using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CameraSystem : ReactiveSystem<GameEntity>
{
    private GameContext _game;
    
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
               _game.currentActorEntity.isPlayer;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var player = _game.currentActorEntity;

        Debug.Log("WATAFAK");
        _game.cameraEntity.view.gameObject.transform.position = new Vector3(player.position.x, 3, player.position.y);
    }
}