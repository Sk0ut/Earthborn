using System.Collections.Generic;
using Entitas;
using Entitas.Unity;

public class AddUiElementSystem : ReactiveSystem<UiEntity>
{
    private readonly Contexts _contexts;
    
    public AddUiElementSystem(Contexts contexts) : base(contexts.ui)
    {
        _contexts = contexts;
    }

    protected override ICollector<UiEntity> GetTrigger(IContext<UiEntity> context)
    {
        return context.CreateCollector(UiMatcher.Ui);
    }

    protected override bool Filter(UiEntity entity)
    {
        return entity.hasUi;
    }

    protected override void Execute(List<UiEntity> entities)
    {
        foreach (var entity in entities)
        {
            var canvas = _contexts.ui.canvas.value;
            canvas.Link(entity, _contexts.ui);
            entity.ui.value.transform.SetParent(canvas.transform);
        }
    }
}