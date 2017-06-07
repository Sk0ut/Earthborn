public sealed class GameSystems : Feature
{
    public GameSystems(Contexts contexts) : base("Game Systems")
    {
        Add(new InitializeGameSystem(contexts));
		Add(new MapSystem(contexts));
		Add(new RandomDamageSystem(contexts));
    }
}