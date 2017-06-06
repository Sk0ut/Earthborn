using UnityEngine;

public static class BlueprintsExtensions
{
    public static GameEntity CreateTile(this GameContext context, Vector2 position, TileType type) {
        var entity = context.CreateEntity();
        entity.AddPosition((int)position.x, (int)position.y);
        entity.AddTile(type);

        return entity;
    }
}