using Entitas;

public class EventCleanupSystem : ICleanupSystem
{

    private readonly GameContext _game;
    private readonly IGroup<GameEntity> _events;

    public EventCleanupSystem(Contexts contexts)
    {
        _game = contexts.game;
        _events = _game.GetGroup(GameMatcher.EventType);
    }

    public void Cleanup()
    {
        foreach (var evEntity in _events.GetEntities())
        {
            evEntity.Destroy();
        }
    }
}