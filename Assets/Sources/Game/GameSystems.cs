public sealed class GameSystems : Feature
{
    public GameSystems(Contexts contexts) : base("Game Systems")
    {
        Add(new InitializeGameSystem(contexts));
		Add(new MapSystem(contexts));

        // Turn based logic
        Add(new ActorManagerSystem(contexts));
        Add(new TurnManagerSystem(contexts));
        
        Add(new AddTurnEnergySystem(contexts));
		Add (new LightEffectSystem (contexts));
        Add(new ActorTurnSystem(contexts));
        Add(new EndTurnSystem(contexts));
        
        // AI controllers
		Add(new AIControlSystem (contexts));

        // Player controllers
        Add(new PlayerMoveController(contexts));
        Add(new PlayerWaitController(contexts));
        Add(new PlayerToggleLightController(contexts));
        Add(new PlayerPickupItemControllerSystem(contexts));
		Add (new PlayerFloorTransitionController (contexts));
        Add(new PlayerAttackController(contexts));
        
        // Actions
        Add(new ActionManagerSystem(contexts));
        Add(new ActionCleanupSystem(contexts));
        
        Add(new MoveActionSystem(contexts));
        Add(new WaitActionSystem(contexts));
		Add(new ToggleLightActionSystem (contexts));
        Add(new PickupItemActionSystem(contexts));
        Add(new AttackActionSystem(contexts));
		Add (new FloorTransitionActionSystem (contexts));

		Add (new ActorDeathSystem (contexts));
        
        // Items
        Add(new CheckStorageCapacitySystem(contexts));
        Add(new ItemCleanupSystem(contexts));

        // Events
        Add(new EventCleanupSystem(contexts));
    }
}