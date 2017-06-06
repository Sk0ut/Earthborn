using Entitas;

public enum TileType
{
    EMPTY,
    GROUND,
    WALL
}

[Game]
public class TileComponent : IComponent
{
    public TileType type;
}