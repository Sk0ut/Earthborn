using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class AnimateAdventurerAttackSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;

    public AnimateAdventurerAttackSystem(Contexts contexts) : base(contexts.game)
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
               entity.eventType.value == Event.ActorAttacked &&
               entity.hasTarget &&
               entity.hasPointing &&
               entity.hasAttackActionHits &&
               entity.target.value.hasUnitType &&
               entity.target.value.unitType.value == Unit.Adventurer &&
               entity.target.value.hasView &&
               entity.target.value.hasPointing &&
               entity.target.value.view.gameObject.GetComponentInChildren<Animator>() != null;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var ev in entities)
        {
            var target = ev.target.value;

            var animator = target.view.gameObject.GetComponentInChildren<Animator>();

            if (target.pointing.direction != ev.pointing.direction)
            {
				target.ReplacePointing(ev.pointing.direction);
            }

            var animation = _game.CreateEntity();
            animation.AddAnimation(CreateAnimation(animator, ev.attackActionHits.value));
            animation.AddAnimationTarget(target);
        }
    }

    IEnumerator CreateAnimation(Animator animator, List<HitData> hits)
    {
        var done = false;
        var eventsController = animator.GetComponent<AdventurerSoundEventsController>();
        eventsController.OnAttackEnd = () => done = true;
        eventsController.OnMeleeAttackHit = () => {
            foreach(var hit in hits)
            {
                var ev = _game.CreateEvent(Event.MeleeAttackHit);
                ev.AddTarget(hit.Target);
            }
        };
        
        animator.SetTrigger("Melee");
        while (!done)
        {
            yield return null;
        }
    }
}