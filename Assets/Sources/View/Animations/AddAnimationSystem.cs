using System.Collections.Generic;
using System.ComponentModel;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class AddAnimationSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;

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
            var inView = InFieldOfView(animationEntity.animationTarget.value.view.gameObject);

            if (animationEntity.hasAnimationTarget &&
                !animationEntity.animationTarget.value.isSeethrough &&
                animationEntity.animationTarget.value.hasView &&
                !inView)
            {
                animationEntity.Destroy();
                continue;
            }
            if (!animationEntity.isImmediate)
            {
                _game.animationQueue.value.Enqueue(animationEntity);
            }

            _game.isAnimating = true;
        }
    }

    private bool InFieldOfView(GameObject target)
    {
        var transform = Camera.main.transform;

        //Cast a ray from this object's transform the the watch target's transform.
        var hits = Physics.RaycastAll(
            transform.position,
            target.transform.position - transform.position,
            Vector3.Distance(target.transform.position, transform.position),
            LayerMask.GetMask("Obstruct")
        );

        //Loop through all overlapping objects and disable their mesh renderer
        foreach (var hit in hits)
        {
            var collider = hit.collider;
            var colliderObj = hit.collider.gameObject;
            var colliderEntity = (GameEntity) colliderObj.GetEntityLink().entity;

            if (colliderEntity == null ||
                !colliderEntity.isObstructable ||
                colliderObj.transform == target ||
                collider.transform.root == target)
                continue;

            return false;
        }
        return true;
    }
}