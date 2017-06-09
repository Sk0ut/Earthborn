using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class AnimateWaitActionSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;

    public AnimateWaitActionSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(
            GameMatcher.EventType
        ));
    }

    protected override bool Filter(GameEntity ev)
    {
        return ev.hasEventType &&
               ev.eventType.value == Event.ActorWaited &&
               ev.hasTarget &&
               ev.target.value.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var ev in entities)
        {
            var target = ev.target.value;
            
            var transform = target.view.gameObject.transform;
            var pos = transform.position;

            var jumpSeq = DOTween.Sequence();

            var jumpUp = transform.DOMove(
                new Vector3(pos.x, pos.y + 0.25f, pos.z),
                0.5f
            ).SetEase(Ease.OutCubic);

            var fallDown = transform.DOMove(
                new Vector3(pos.x, pos.y, pos.z),
                0.5f
            ).SetEase(Ease.InCubic);

            jumpSeq.Append(jumpUp)
                .Append(fallDown)
                .Pause();


            var animation = _game.CreateEntity();
            animation.AddAnimation(CreateAnimation(jumpSeq));
        }
    }

    IEnumerator CreateAnimation(Sequence tween)
    {
        var done = false;
        tween.OnStart(() =>
        {
            //Debug.Log("Move started " + DateTime.Now);
        });
        tween.OnComplete(() =>
        {
            //Debug.LogWarning("IM FUKIN DUNE " + DateTime.Now);
            done = true;
        });

        tween.Play();
        while (!done)
        {
            yield return null;
        }
    }
}