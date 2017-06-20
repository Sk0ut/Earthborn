using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class MinionBossAttackSoundSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    
    public MinionBossAttackSoundSystem(Contexts contexts) : base(contexts.game)
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
               entity.target.value.unitType.value == Unit.MinionBoss &&
               entity.target.value.hasView;
    }

    protected override void Execute(List<GameEntity> events)
    {
        foreach (var evt in events)
        {
            var audioSource = evt.target.value.view.gameObject.GetComponent<AudioSource>();
            var file = Resources.Load<AudioClip>("Sounds/eb_monster_attack");
            audioSource.PlayOneShot(file);
        }
        
    }
}