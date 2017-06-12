using Entitas;

public class ClearCommandsSystem : ICleanupSystem
{
    private readonly Contexts _contexts;
    private readonly IGroup<InputEntity> _commands;

    public ClearCommandsSystem(Contexts contexts)
    {
        _contexts = contexts;
        _commands = contexts.input.GetGroup(InputMatcher.Command);
    }

    public void Cleanup()
    {
        foreach (var command in _commands.GetEntities())
        {
            command.Destroy();
        }
    }
}