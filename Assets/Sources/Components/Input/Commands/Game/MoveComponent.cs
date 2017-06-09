using Entitas;

public enum MoveDirection
{
    Left,
    Up,
    Right,
    Down
}

[Input]
public class MoveComponent : IComponent
{
    public MoveDirection value;
}