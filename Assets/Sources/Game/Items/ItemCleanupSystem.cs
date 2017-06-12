using Entitas;

public class ItemCleanupSystem : ICleanupSystem
{
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _items;

    public ItemCleanupSystem(Contexts contexts)
    {
        _contexts = contexts;
        
        // Every item that is neither in a storage on placed in the world
        _items = contexts.game.GetGroup(
            GameMatcher
                .AllOf(GameMatcher.Item)
                .NoneOf(GameMatcher.StorageSource, GameMatcher.Position));
    }

    public void Cleanup()
    {
        foreach (var item in _items.GetEntities())
        {
            item.Destroy();
        }
    }
}