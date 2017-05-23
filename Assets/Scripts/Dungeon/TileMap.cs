using UnityEngine;
using System.Collections;

public class TileMap
{
	private Tile[,] _tiles;

	public Tile[,] tiles {
		get {
			return _tiles;
		}
		set {
			_tiles = value;
		}
	}

	public TileMap(Vector2 size, Tile defaultTile) {
		this.tiles = new Tile[(int)size.x, (int)size.y];
		fillDefault (defaultTile);
	}

	private void fillDefault(Tile defaultTile) {
		for (int x = 0; x < this.tiles.GetLength (0); ++x) {
			for (int y = 0; y < this.tiles.GetLength (1); ++y) {
				this.tiles [x, y] = defaultTile;
			}
		}
	}
}
		