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


        var pistol = _context.CreateEntity();
        pistol.isItem = true;
        pistol.AddItemId(ItemId.BasePistol);
        pistol.AddItemStats("X375-R", "Most powerful pistol in all of Earth", 100);
        pistol.isEquipableItem = true;
        pistol.isGun = true;
        pistol.AddAsset(_context.assets.value.Pistol);
        pistol.AddPosition(6, 9);
        pistol.isSeethrough = true;
        
        var pistol2 = _context.CreateEntity();
        pistol2.isItem = true;
        pistol2.AddItemId(ItemId.BasePistol);
        pistol2.AddItemStats("X375-X", "Most powerful pistol in all of Earth 2", 120);
        pistol2.isEquipableItem = true;
        pistol2.isGun = true;
        pistol2.AddAsset(_context.assets.value.Pistol);
        pistol2.AddPosition(8, 8);
        pistol2.isSeethrough = true;
    }
}