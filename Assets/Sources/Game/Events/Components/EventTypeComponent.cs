using Entitas;

public enum Event
{
    ActorWaited,
    ActorWalked,
    ActorPickedItem,
    Footstep
}

[Game]
public class EventTypeComponent : IComponent
{
    public Event value;
}