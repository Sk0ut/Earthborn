using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using Entitas;
using UnityEngine;
using Debug = UnityEngine.Debug;

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
               entity.eventType.value == Event.MeleeAttackHit &&
               entity.hasTarget &&
               entity.target.value.hasUnitType &&
               entity.target.value.unitType.value == Unit.MinionBoss &&
               entity.target.value.isDead &&
               entity.target.value.hasView &&
               entity.target.value.view.gameObject.GetComponentInChildren<Animator>() != null;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var ev in entities)
        {
            var target = ev.target.value;

            var animator = target.view.gameObject.GetComponentInChildren<Animator>();
            var co = _game.CreateEntity();
            co.AddCoroutine(CreateAnimation(target, animator));
        }
    }

    IEnumerator CreateAnimation(GameEntity entity, Animator animator)
    {
        var sw = new Stopwatch();
        sw.Start();
        animator.SetTrigger("Die");
        while (sw.ElapsedMilliseconds <= 5000) yield return null;

        var sink = entity.view.gameObject.transform.DOMoveY(-0.5f, 2f)
            .SetEase(Ease.Linear)
            .Play();
        
        while (sink.IsPlaying()) yield return null;
        Debug.Log("ded");
        
        entity.Destroy();
    }
}