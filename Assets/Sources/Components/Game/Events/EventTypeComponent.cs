using Entitas;

public enum Event
{
    ActorWaited,
}

[Game]
public class EventTypeComponent : IComponent
{
    public Event value;
}