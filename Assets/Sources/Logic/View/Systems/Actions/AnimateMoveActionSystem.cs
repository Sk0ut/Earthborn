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
        return context.CreateCollector(GameMatcher.AllOf(
            GameMatcher.Position
        ));
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
            var tween = transform.DOMove(
                new Vector3(pos.x, transform.position.y, pos.y),
                e.easing.duration
            ).SetEase(Ease.Linear).Pause();

            var animation = _game.CreateEntity();
            animation.AddAnimation(CreateAnimation(tween));
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