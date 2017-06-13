using System.Collections.Generic;
using Entitas;

public class CheckStorageCapacitySystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    
    public CheckStorageCapacitySystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.StorageSource);
    }

    protected override bool Filter(GameEntity item)
    {
        return item.isItem &&
               item.hasStorageSource &&
               item.storageSource.value.isStorage;
    }

    protected override void Execute(List<GameEntity> items)
    {
        foreach (var item in items)
        {
            var storageEty = item.storageSource.value;
            var storageItems = _contexts.game.GetEntitiesWithStorageSource(storageEty);

            if (storageEty.hasStorageCapacity &&
                storageItems.Count >= storageEty.storageCapacity.value)
            {
                item.RemoveStorageSource();
            }
        }
    }
}