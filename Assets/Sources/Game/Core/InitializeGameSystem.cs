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
        camera.isVisible = true;

        var player = _context.CreatePlayer(new UnityEngine.Vector2());
        _context.CreateLight(true, _context.globals.value.PlayerLightingRadius, player);

        /*
        var player2 = _context.CreatePlayer(new UnityEngine.Vector2 (5, 3));
        player2.isPlayer = false;
        player2.ReplaceActorSpeed(0.25f);
        player2.AddAIControl (AIControlType.RANDOM);
        _context.CreateLight (true, _context.globals.value.PlayerLightingRadius, player2);
        */
        
        var pistol = _context.CreateEntity();
        pistol.isItem = true;
        pistol.AddItemId(ItemId.BasePistol);
        pistol.AddItemStats("X375-R", "Most powerful pistol in all of Earth", 100);
        pistol.isEquipableItem = true;
        pistol.isGun = true;
        pistol.AddAsset(_context.assets.value.Pistol);
        pistol.AddPosition(6, 9);
    }
}