using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AdventurerAttackSoundSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    
    public AdventurerAttackSoundSystem(Contexts contexts) : base(contexts.game)
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
               entity.eventType.value == Event.ActorAttacked &&
               entity.hasAttackActionHits &&
               entity.hasTarget &&
               entity.target.value.hasUnitType &&
               entity.target.value.unitType.value == Unit.Adventurer &&
               entity.target.value.hasView;
    }

    protected override void Execute(List<GameEntity> events)
    {
        foreach (var evt in events)
        {
            var isHit = evt.attackActionHits.value.Count > 0;
            Debug.Log("Adventurer attack: " + isHit);

            var audioSource = evt.target.value.view.gameObject.GetComponent<AudioSource>();
            audioSource.clip = Resources.Load<AudioClip>("Sounds/eb_hero_attack_" + (isHit ? "monster" : "empty"));
            audioSource.time = 0.35f;
            audioSource.Play();
        }
        
    }
}