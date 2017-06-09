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
        _context.SetTurnState(TurnState.Waiting);
        var camera = _context.CreateEntity();
        camera.isCamera = true;
        camera.AddAsset(_context.assets.value.Camera);
        camera.AddPosition(2, 0);
        
		var player = _context.CreatePlayer(new UnityEngine.Vector2 (3, 3));
        
		var player2 = _context.CreatePlayer(new UnityEngine.Vector2 (5, 3));
        player2.isPlayer = false;
    }
}