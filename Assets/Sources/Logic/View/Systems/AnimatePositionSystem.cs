using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class AnimationSystem : ReactiveSystem<GameEntity>
{
    private GameContext _context;
    
    public AnimationSystem(Contexts contexts) : base(contexts.game)
    {
        this._context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition &&
               entity.hasAsset &&
               entity.hasEasing;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var pos = e.position;
            var transform = e.view.gameObject.transform;
            transform
                .DOMove(new Vector3(pos.x, 0f, pos.y), e.easing.duration);
        }
    }
}