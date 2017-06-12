using Entitas;

public class InitializeViewSystem : IInitializeSystem
{
    private GameContext _context;
    
    public InitializeViewSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Initialize()
    {
    }
}