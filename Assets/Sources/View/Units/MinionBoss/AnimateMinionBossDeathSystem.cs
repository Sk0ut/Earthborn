using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class AnimateMinionBossDeathSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;

    public AnimateMinionBossDeathSystem(Contexts contexts) : base(contexts.game)
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
               entity.eventType.value == Event.ActorDied &&
               entity.hasTarget &&
               entity.target.value.hasUnitType &&
               entity.target.value.unitType.value == Unit.MinionBoss &&
               entity.target.value.hasView &&
               entity.target.value.view.gameObject.GetComponentInChildren<Animator>() != null;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var ev in entities)
        {
            var target = ev.target.value;

            var animator = target.view.gameObject.GetComponentInChildren<Animator>();
            var animation = _game.CreateEntity();
            //animation.AddAnimation(CreateAnimation(seq));
            animation.AddAnimationTarget(target);
        }
    }

    IEnumerator CreateAnimation(Sequence seq)
    {
        var done = false;
        seq.OnStart(() =>
        {
            //Debug.Log("Move started " + DateTime.Now);
        });
        seq.OnComplete(() =>
        {
            done = true;
        });

        seq.Play();
        while (!done)
        {
            yield return null;
        }
    }
}