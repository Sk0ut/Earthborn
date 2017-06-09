using System;
using Entitas;
class MapSystem : IInitializeSystem, IExecuteSystem
{
    private readonly GameContext _context;
    

    public MapSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Initialize()
    {
		int[,] map = new int[,] { 
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
		 	{1, 0, 0, 0, 0, 1, 1, 1, 1, 1},
			{1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{1, 0, 0, 0, 0, 1, 1, 1, 0, 1},
			{1, 0, 1, 1, 0, 1, 1, 1, 0, 1},
			{1, 0, 1, 1, 0, 1, 1, 1, 0, 1},
			{1, 0, 1, 1, 0, 0, 1, 1, 0, 1},
			{1, 0, 1, 1, 1, 0, 0, 1, 0, 1},
			{1, 0, 1, 1, 1, 0, 0, 1, 0, 1},
			{1, 0, 0, 0, 1, 1, 1, 0, 0, 1},
			{1, 0, 0, 0, 1, 1, 1, 0, 1, 1},
			{1, 0, 0, 0, 0, 0, 0, 0, 1, 1},
			{1, 0, 0, 0, 1, 0, 0, 0, 1, 1},
			{1, 1, 1, 1, 1, 0, 0, 0, 1, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
		};

		for (var y = 0; y < map.GetLength(0); ++y)
        {
			for (var x = 0; x < map.GetLength(1); ++x)
            {
				TileType type;
				UnityEngine.GameObject asset;
				bool blocking;
				switch (map[y, x]) {
				case 0:
					type = TileType.GROUND;
					asset = _context.assets.value.GroundTile;
					blocking = false;
					break;
				case 1:
				default:
					type = TileType.WALL;
					asset = _context.assets.value.Wall;
					blocking = true;
					break;
				}
				_context.CreateTile (new UnityEngine.Vector2 (x, map.GetLength (0) - y), type, blocking, asset);
            }
        }
    }
    
    public void Execute()
    {
    }
}