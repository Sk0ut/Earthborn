using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PlayerFootstepsSoundSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    
    public PlayerFootstepsSoundSystem(Contexts contexts) : base(contexts.game)
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
               entity.eventType.value == Event.Footstep &&
               entity.hasTarget &&
               entity.target.value.isPlayer &&
               entity.target.value.hasView;
    }

    protected override void Execute(List<GameEntity> events)
    {
        foreach (var evt in events)
        {
            AudioSource.PlayClipAtPoint(
                Resources.Load<AudioClip>("Sounds/footstep_" + Random.Range(1, 5)),
                evt.target.value.view.gameObject.transform.position
            );  
        }
        
    }
}