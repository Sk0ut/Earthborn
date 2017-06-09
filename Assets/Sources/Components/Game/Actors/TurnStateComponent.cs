using Entitas;
using Entitas.CodeGeneration.Attributes;

public enum TurnState
{
    Waiting,
    StartTurn,
    AskAction,
    PerformAction,
    EndTurn
}

[Game, Unique]
public class TurnStateComponent : IComponent
{
    public TurnState value;
}