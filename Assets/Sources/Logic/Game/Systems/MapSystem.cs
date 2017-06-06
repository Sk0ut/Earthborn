using System;
using Entitas;
class MapSystem : IInitializeSystem, IExecuteSystem
{
    private readonly GameContext _context;
    
    private IGroup<GameEntity> _tiles;
    private IGroup<GameEntity> _tileMaps;

    public MapSystem(Contexts contexts)
    {
        _context = contexts.game;
        _tiles = _context.GetGroup(GameMatcher.AllOf(GameMatcher.Tile, GameMatcher.Position));
        _tileMaps = _context.GetGroup(GameMatcher.AllOf(GameMatcher.TileMap, GameMatcher.Position));
    }

    public void Initialize()
    {
        for (var y = 0; y < 5; ++y)
        {
            for (var x = 0; x < 10; ++x)
            {
                var tile = _context.CreateEntity();
                tile.AddPosition(x, y);
                tile.AddTile(TileType.GROUND);
                tile.AddAsset(_context.assets.value.GroundTile);
            }
        }
    }
    
    public void Execute()
    {
    }
}