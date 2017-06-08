using Entitas;

public enum ActorState
{
    INITIALIZING,
    WAITING,
    TURN,
    ACTING,
    ACTED
}

[Game]
public class ActorStateComponent : IComponent
{
    public ActorState value;
}