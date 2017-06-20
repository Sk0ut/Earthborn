using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using Entitas;
using UnityEngine;

public class AdventurerFootstepsSoundSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    
    public AdventurerFootstepsSoundSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
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
               entity.target.value.hasUnitType &&
               entity.target.value.unitType.value == Unit.Adventurer &&
               entity.target.value.hasView;
    }

    protected override void Execute(List<GameEntity> events)
    {
        foreach (var evt in events)
        {
            var audioSource = evt.target.value.view.gameObject.GetComponent<AudioSource>();

            var co = _contexts.game.CreateEntity();
            co.AddCoroutine(PlayFootStepSound(audioSource));
        }
    }

    private IEnumerator PlayFootStepSound(AudioSource audioSource)
    {
        var sw = new Stopwatch();
        sw.Start();
        while (sw.ElapsedMilliseconds <= 230) yield return null;
        audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/eb_hero_steps/eb_hero_step_" + Random.Range(1, 7)));
    }
}