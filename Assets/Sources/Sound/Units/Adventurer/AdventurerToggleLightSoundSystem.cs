using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AdventurerToggleLightSoundSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    
    public AdventurerToggleLightSoundSystem(Contexts contexts) : base(contexts.game)
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
               entity.eventType.value == Event.ActorToggleLight &&
               entity.hasTarget &&
               entity.target.value.isPlayer &&
               entity.target.value.hasView;
    }

    protected override void Execute(List<GameEntity> events)
    {
        foreach (var evt in events)
        {
            AudioSource.PlayClipAtPoint(
                Resources.Load<AudioClip>("Sounds/eb_hero_switch_light"),
                evt.target.value.view.gameObject.transform.position
            );  
        }
        
    }
}