using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class PickupItemActionSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _pickable;

    public PickupItemActionSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _pickable = contexts.game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Item,
                GameMatcher.Position)
            .NoneOf(GameMatcher.StorageSource));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.PickupItemAction);
    }

    protected override bool Filter(GameEntity action)
    {
        var target = action.hasTarget ? action.target.value : null;

        return action.isAction &&
               action.isPickupItemAction &&
               target != null &&
               target.hasPosition &&
               _contexts.game.GetEntitiesWithInventoryOwner(target).Count > 0;
    }

    protected override void Execute(List<GameEntity> actions)
    {
        foreach (var action in actions)
        {
            var target = action.target.value;
            var inventory = _contexts.game.GetEntitiesWithInventoryOwner(target).First();

            if (!inventory.isStorage) continue;

            if (!action.hasItemTarget)
            {
                var item = _pickable.GetEntities().First(i => i.position.x == target.position.x &&
                                                              i.position.y == target.position.y);

                if (item == null) continue;

                if (inventory.hasStorageCapacity)
                {
                    var items = _contexts.game.GetEntitiesWithStorageSource(inventory);
                    if (items.Count >= inventory.storageCapacity.value)
                    {
                        Debug.Log("Inventory full, can't pick up!");
                        continue;
                    }
                }
                
                item.AddStorageSource(inventory);
                item.RemovePosition();
                Debug.Log(target.creationIndex + " has picked up item " + item.creationIndex);
                
                target.ReplaceActorEnergy(target.actorEnergy.energy - 1f);
                _contexts.game.ReplaceTurnState(TurnState.EndTurn);
                action.Destroy();
            }
        }
    }
}