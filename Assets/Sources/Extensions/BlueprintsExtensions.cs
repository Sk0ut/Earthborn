using UnityEngine;

public static class BlueprintsExtensions
{
    public static GameEntity CreateTile(this GameContext context, Vector2 position, TileType type) {
        var entity = context.CreateEntity();
        entity.AddPosition((int)position.x, (int)position.y);
        entity.AddTile(type);

        return entity;
    }

	public static GameEntity CreatePlayer(this GameContext context, Vector2 position) {
		var entity = context.CreateEntity ();
		entity.isPlayer = true;
		entity.AddHealth (context.globals.value.PlayerHealth);
		entity.AddAsset(context.assets.value.Player);
		entity.AddPosition((int)position.x, (int)position.y);
		entity.AddEasing(0.5f);

		return entity;
	}
}