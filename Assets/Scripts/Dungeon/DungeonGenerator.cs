using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;

public class DungeonGenerator : MonoBehaviour
{
	private class Sector
	{
		private Vector2 _start;
		private Vector2 _size;

		public Vector2 start
		{
			get { return _start; }
			set { _start = value; }
		}

		public Vector2 size
		{
			get { return _size; }
			set { _size = value; }
		}

		public Sector(Vector2 start, Vector2 size)
		{
			this.start = start;
			this.size = size;
		}
	}


	private TileMap tileMap;
	public Vector2 size = new Vector2(40, 40);
	public int minSectorSize = 10;
	public int divisionMargin = 1;
	public float strictness = 0.2f;
	private GameObject wallPrefab;
	private GameObject wallPrefab2;
	private Transform mapHolder;
	private System.Random rnd = new System.Random();
	public int minRoomHeight;
	public int minRoomWidth;

	// Use this for initialization
	void Start()
	{
		wallPrefab = Resources.Load("Wall") as GameObject;
		wallPrefab2 = Resources.Load("WallTest") as GameObject;
		StartCoroutine(waitForDungeon());
	}

	IEnumerator waitForDungeon()
	{
		Thread dungeonThread = new Thread(new ThreadStart(generateDungeon));
		dungeonThread.Start();
		while (dungeonThread.IsAlive)
		{
			yield return null;
		}
		placeWalls();
	}

	private void placeWalls()
	{
		mapHolder = new GameObject("Board").transform;
		mapHolder.position = new Vector3(0.5f, 0.5f, 0.5f);
		for (int x = divisionMargin; x < tileMap.tiles.GetLength(0) - divisionMargin; ++x)
		{
			for (int y = divisionMargin; y < tileMap.tiles.GetLength(1) - divisionMargin; ++y)
			{
				if (tileMap.tiles[x, y] == Tile.WALL)
				{
					GameObject wallInstance = Instantiate(wallPrefab) as GameObject;
					wallInstance.transform.parent = mapHolder;
					wallInstance.transform.localPosition = new Vector3(x, 0, y);
				}
				else if (tileMap.tiles[x, y] == Tile.NON_FLOOR)
				{
					GameObject wallInstance = Instantiate(wallPrefab2) as GameObject;
					wallInstance.transform.parent = mapHolder;
					wallInstance.transform.localPosition = new Vector3(x, 0, y);
				}
			}
		}
	}

	private void generateDungeon()
	{
		tileMap = new TileMap(size, Tile.WALL);
		Queue<Sector> sectors = new Queue<Sector>();
		sectors.Enqueue(new Sector(new Vector2(1, 1), new Vector2(size.x - 2, size.y - 2)));
		IList<Sector> finalSectors = new List<Sector>();

		while (sectors.Count != 0)
		{
			Sector sector = sectors.Dequeue();

			if (sector.size.x < sector.size.y)
			{
				splitY(sector, sectors, finalSectors);
			}
			else if (sector.size.x > sector.size.y)
			{
				splitX(sector, sectors, finalSectors);
			}
			else if (rnd.Next(0, 1) == 0)
			{
				splitY(sector, sectors, finalSectors);
			}
			else
			{
				splitX(sector, sectors, finalSectors);
			}
		}

		foreach (Sector sector in finalSectors)
		{
			Tile tile = Tile.NON_FLOOR;
			Vector2 start = Vector2.zero;

			if (rnd.NextDouble () < 1) {
				start = new Vector2(rnd.Next(0, (int) (size.x - minRoomWidth)), rnd.Next(0, (int)(size.y - minRoomHeight)));
				size = new Vector2 (rnd.Next (minRoomWidth, (int)(size.x - start.x)), rnd.Next(minRoomHeight, (int)(size.y - start.y)));
				tile = Tile.FLOOR;
			}

			for (int x = 0; x < size.x; ++x)
			{
				for (int y = 0; y < size.y; ++y)
				{
					tileMap.tiles[(int) (sector.start.x + divisionMargin + start.x + x), (int) (sector.start.y + divisionMargin + start.y + y)] = tile;
				}
			}
		}
	}

	void splitY(Sector sector, Queue<Sector> sectors, IList<Sector> finalSectors)
	{
		if (sector.size.y < 2 * minSectorSize + 1)
		{
			finalSectors.Add(sector);
			return;
		}
		int bound = Mathf.Max(minSectorSize, (int) (sector.size.y * strictness));
		int splitSize = rnd.Next(bound, (int) (sector.size.y - bound - 1));

		sectors.Enqueue(new Sector(sector.start, new Vector2(sector.size.x, splitSize)));
		sectors.Enqueue(new Sector(new Vector2(sector.start.x, sector.start.y + splitSize + 1),
			new Vector2(sector.size.x, sector.size.y - splitSize - 1)));
	}

	void splitX(Sector sector, Queue<Sector> sectors, IList<Sector> finalSectors)
	{
		if (sector.size.x < 2 * minSectorSize + 1)
		{
			finalSectors.Add(sector);
			return;
		}
		int bound = Mathf.Max(minSectorSize, (int) (sector.size.x * strictness));
		int splitSize = rnd.Next(bound, (int) (sector.size.x - bound - 1));


		sectors.Enqueue(new Sector(sector.start, new Vector2(splitSize, sector.size.y)));
		sectors.Enqueue(new Sector(new Vector2(sector.start.x + splitSize + 1, sector.start.y),
			new Vector2(sector.size.x - splitSize - 1, sector.size.y)));
	}
}