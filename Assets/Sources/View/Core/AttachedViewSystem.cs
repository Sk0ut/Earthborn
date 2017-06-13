using Entitas;

public class AttachedViewSystem : ReactiveSystem<GameEntity>
{
    public AttachedViewSystem(Contexts context) : base(context.game)
    {
    }


    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(
            GameMatcher.AttachedTo,
            GameMatcher.View
        ));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasAttachedTo &&
               entity.hasView &&
               entity.attachedTo.value.hasView;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.view.gameObject.transform.SetParent(entity.attachedTo.value.view.gameObject.transform);
        }
    }
}