using Entitas;

public class InitializeGameSystem : IInitializeSystem
{
    private GameContext _context;
    
    public InitializeGameSystem(Contexts contexts)
    {
        _context = contexts.game;
    }
    public void Initialize()
    {
        var player = _context.CreateEntity();
        player.isPlayer = true;
        player.AddAsset(_context.assets.value.Player);
        player.AddPosition(0, 0);
        player.AddEasing(0.5f);
    }
}