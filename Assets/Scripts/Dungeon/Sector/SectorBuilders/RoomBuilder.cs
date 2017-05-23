using UnityEngine;

public class RoomBuilder : ISectorBuilder
{
	public RoomBuilder();

	public void build(DungeonGenerator dungeonGenerator, Vector2 start, Vector2 size);
}

