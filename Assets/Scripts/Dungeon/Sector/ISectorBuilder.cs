using UnityEngine;

public interface ISectorBuilder
{
	void build(TileMap tilemap, Vector2 start, Vector2 size, out Vector2[] exits);
}
	