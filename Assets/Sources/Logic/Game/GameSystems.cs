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
        Add(new EndTurnSystem(contexts));
        
        Add(new MapSystem(contexts));
		Add(new AIControlSystem (contexts));

        // Player controllers
        Add(new PlayerMoveController(contexts));
        Add(new PlayerWaitController(contexts));
        
        // Actions
        Add(new ActionManagerSystem(contexts));
        Add(new ActionCleanupSystem(contexts));
        
        Add(new MoveActionSystem(contexts));
        Add(new WaitActionSystem(contexts));
		Add(new ToggleLightSystem (contexts));

        // Events
        Add(new EventCleanupSystem(contexts));
    }
}