using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class RemoveViewSystem : ReactiveSystem<GameEntity>
{
    public RemoveViewSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Asset.Removed());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var gameObject = e.view.gameObject;
            Debug.Log(gameObject);
            gameObject.Unlink();
            Object.Destroy(gameObject);
            e.RemoveView();
        }
    }
}