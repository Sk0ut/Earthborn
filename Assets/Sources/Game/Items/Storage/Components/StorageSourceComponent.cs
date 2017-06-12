using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public class StorageSourceComponent : IComponent
{
    [EntityIndex]
    public GameEntity value;
}