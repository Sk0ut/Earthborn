using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class DrawDroppedItemSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    
    public DrawDroppedItemSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position.Added(), GameMatcher.StorageSource.Removed());
    }

    protected override bool Filter(GameEntity item)
    {
        return item.isItem &&
               item.hasView &&
               !item.hasStorageSource &&
               item.hasPosition;
    }

    protected override void Execute(List<GameEntity> droppedItems)
    {
        foreach (var item in droppedItems)
        {
            var gameObject = item.view.gameObject;
            gameObject.SetActive(true);
            var pos = item.position;
            gameObject.transform.position = new Vector3(pos.x, 0f, pos.y);
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            gameObject.transform.localScale = Vector3.one * 2f;
        }
    }
}