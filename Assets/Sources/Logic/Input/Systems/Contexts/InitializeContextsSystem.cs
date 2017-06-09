using Entitas;

public class InitializeContextsSystem : IInitializeSystem
{
    private InputContext _input;
    
    public InitializeContextsSystem(Contexts contexts)
    {
        _input = contexts.input;
    }
    
    public void Initialize()
    {
        _input.SetGameContext(true);
    }
}