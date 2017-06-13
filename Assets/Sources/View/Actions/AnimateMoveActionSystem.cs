using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class AnimateMoveActionSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;

    public AnimateMoveActionSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.EventType);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasEventType &&
               entity.eventType.value == Event.ActorWalked &&
               entity.hasTarget &&
               entity.target.value.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var ev in entities)
        {
            var target = ev.target.value;

            var pos = target.position;
            var transform = target.view.gameObject.transform;
            var tween = transform.DOMove(
                new Vector3(pos.x, transform.position.y, pos.y),
                target.easing.duration
            ).SetEase(Ease.Linear).Pause();

            var animation = _game.CreateEntity();
            animation.AddAnimation(CreateAnimation(tween));
            animation.AddAnimationTarget(target);

            // Handled
            ev.Destroy();
        }
    }

    IEnumerator CreateAnimation(Tweener tween)
    {
        var done = false;
        tween.OnStart(() =>
        {
            //Debug.Log("Move started " + DateTime.Now);
        });
        tween.OnComplete(() =>
        {
            done = true;
        });

        tween.Play();
        while (!done)
        {
            yield return null;
        }
    }
}