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
        
		var player = _context.CreatePlayer(new UnityEngine.Vector2 (6, 4));
		_context.CreateLight (true, _context.globals.value.PlayerLightingRadius, player);
        
		var player2 = _context.CreatePlayer(new UnityEngine.Vector2 (8, 6));
        player2.ReplaceActorSpeed(0.25f);
		player2.AddAIControl (AIControlType.RANDOM);
		_context.CreateLight (true, _context.globals.value.PlayerLightingRadius, player2);
    }
}