using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class AnimatePlayerAttackSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;

    public AnimatePlayerAttackSystem(Contexts contexts) : base(contexts.game)
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
            animation.AddAnimation(CreateAnimation(animator));
            
            // Handled
            ev.Destroy();
        }
    }

    IEnumerator CreateAnimation(Animator animator)
    {
        animator.SetTrigger("Melee");
        
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Melee Attack"))
        {
            yield return null;
        }
    }
}