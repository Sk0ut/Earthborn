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
                var tile = _context.CreateEntity();
				tile.AddPosition(x, map.GetLength(0) - y);
				switch (map[y, x]) {
				case 0:
					tile.AddTile (TileType.GROUND);
					tile.AddAsset (_context.assets.value.GroundTile);
					break;
				case 1:
					tile.AddTile (TileType.WALL);
					tile.AddAsset (_context.assets.value.Wall);
					break;
				}
            }
        }
    }
    
    public void Execute()
    {
    }
}