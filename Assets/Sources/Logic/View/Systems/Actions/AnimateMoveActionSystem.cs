using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class AnimateMoveActionSystem : ReactiveSystem<GameEntity>
{
    public AnimateMoveActionSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition &&
               entity.hasAsset &&
               entity.hasEasing &&
               entity.hasActorState &&
               entity.actorState.value == ActorState.ACTED;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var pos = e.position;
            var transform = e.view.gameObject.transform;
            var tween = transform.DOMove(
                new Vector3(pos.x, transform.position.y, pos.y),
                e.easing.duration
            ).SetEase(Ease.Linear).Pause();
            
            e.AddAnimation(CreateAnimation(tween));
        }
    }

    IEnumerator CreateAnimation(Tweener tween)
    {
        tween.Play();
        Debug.Log("Animation started");
        yield return tween.WaitForCompletion();
        Debug.Log("Animation finished");
    }
}