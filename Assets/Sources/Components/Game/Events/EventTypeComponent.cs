using Entitas;

public enum Event
{
    ActorWaited,
    ActorWalked,
}

[Game]
public class EventTypeComponent : IComponent
{
    public Event value;
}