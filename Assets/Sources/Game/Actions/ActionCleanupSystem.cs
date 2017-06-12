using System.Diagnostics;
using Entitas;

public class ActionCleanupSystem : ICleanupSystem
{
    private readonly GameContext _context;
    private readonly IGroup<GameEntity> _unhandledActions;

    public ActionCleanupSystem(Contexts contexts)
    {
        _context = contexts.game;
        _unhandledActions = _context.GetGroup(GameMatcher.Action);
    }

    public void Cleanup()
    {
        foreach (var action in _unhandledActions.GetEntities())
        {
            action.Destroy();
        }
    }
}