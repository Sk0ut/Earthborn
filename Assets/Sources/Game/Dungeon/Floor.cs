using System;

public class Floor
{
	public readonly int [,] tiles;
	public readonly UnityEngine.Vector2 start;
	public readonly UnityEngine.Vector2 end;

	public Floor(int[,] tiles, UnityEngine.Vector2 start, UnityEngine.Vector2 end) {
		this.tiles = tiles;
		this.start = start;
		this.end = end;
	}
}

