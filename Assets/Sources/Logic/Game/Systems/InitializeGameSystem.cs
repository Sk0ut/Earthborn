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
		var player = _context.CreatePlayer (new UnityEngine.Vector2 (3, 3));
    }
}