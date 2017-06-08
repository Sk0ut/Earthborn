using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AddAnimationSystem : ReactiveSystem<GameEntity>
{
    private GameContext _game;
    public AddAnimationSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Animation);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasAnimation;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var animationEntity in entities)
        {
            _game.animationQueue.value.Enqueue(animationEntity.animation.value);
            animationEntity.RemoveAnimation();
        }
    }
}