using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public class InventoryOwnerComponent : IComponent
{
    [EntityIndex]
    public GameEntity value;
}