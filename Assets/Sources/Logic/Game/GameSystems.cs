public sealed class GameSystems : Feature
{
    public GameSystems(Contexts contexts) : base("Game Systems")
    {
        Add(new InitializeGameSystem(contexts));
        
        // Turn based logic
        Add(new ActorManagerSystem(contexts));
        Add(new TurnManagerSystem(contexts));
        
        Add(new AddTurnEnergySystem(contexts));
        Add(new ActorTurnSystem(contexts));
        
        Add(new CameraSystem(contexts));
        
        Add(new MapSystem(contexts));
        //Add(new RandomDamageSystem(contexts));

        // Player controllers
        Add(new PlayerMoveController(contexts));
        
        // Actions
        Add(new MoveActionSystem(contexts));

        Add(new EndTurnCleanupSystem(contexts));
    }
}