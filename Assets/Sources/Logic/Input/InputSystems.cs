public class InputSystems : Feature
{
    public InputSystems(Contexts contexts) : base("Input Systems")
    {
        Add(new KeyboardInputSystem(contexts));
        Add(new AxisInputSystem(contexts));
        
        // Contexts
        Add(new InitializeContextsSystem(contexts));
        Add(new GameContextSystem(contexts));

        Add(new ClearInputSystem(contexts));
    }
}