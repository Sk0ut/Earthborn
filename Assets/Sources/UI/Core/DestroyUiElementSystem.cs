using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class DestroyUiElementSystem : ReactiveSystem<UiEntity>
{
    private readonly Contexts _contexts;
    
    public DestroyUiElementSystem(Contexts contexts) : base(contexts.ui)
    {
        _contexts = contexts;
    }

    protected override ICollector<UiEntity> GetTrigger(IContext<UiEntity> context)
    {
        return context.CreateCollector(UiMatcher.Destroyed);
    }

    protected override bool Filter(UiEntity entity)
    {
        return entity.isDestroyed &&
               entity.hasUi;
    }

    protected override void Execute(List<UiEntity> entities)
    {
        foreach (var ety in entities)
        {
            var go = ety.ui.value;
            go.Unlink();
            Object.Destroy(go);
            ety.Destroy();
        }
    }
}