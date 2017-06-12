using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class DisablePickedItemViewSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    
    public DisablePickedItemViewSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position.Removed());
    }

    protected override bool Filter(GameEntity item)
    {
        return item.isItem &&
               item.hasView &&
               !item.hasPosition;
    }

    protected override void Execute(List<GameEntity> pickedItems)
    {
        foreach (var item in pickedItems)
        {
            item.view.gameObject.SetActive(false);
            var gameObject = item.view.gameObject;
        }
    }
}