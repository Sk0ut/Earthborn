using Entitas;

[Game]
public class PickupItemEventComponent : IComponent
{
    public GameEntity picker;
    public GameEntity item;
}