using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PickupItemSoundSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public PickupItemSoundSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.PickupItemEvent);
    }

    protected override bool Filter(GameEntity ev)
    {
        return ev.hasPickupItemEvent &&
               ev.pickupItemEvent.picker.hasView;
    }

    protected override void Execute(List<GameEntity> events)
    {
        var clip = Resources.Load<AudioClip>("Sounds/eb_hero_pick");

        foreach (var ev in events)
        {
            AudioSource.PlayClipAtPoint(clip, ev.pickupItemEvent.picker.view.gameObject.transform.position);
        }
    }
}