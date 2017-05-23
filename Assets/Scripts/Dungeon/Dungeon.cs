using UnityEngine;
using System.Collections;

public class Dungeon
{
	private TileMap _tileMap;

	public TileMap tileMap {
		get {
			return _tileMap;
		}
		set {
			_tileMap = value;
		}
	}

	public Dungeon(Vector2 size) {
		tileMap = new TileMap (size, Tile.WALL);
	}
}

