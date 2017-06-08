using Entitas;
using UnityEngine;

public class EndTurnCleanupSystem : ICleanupSystem
{
    private GameContext _context;

    public EndTurnCleanupSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Cleanup()
    {
        if (!_context.hasCurrentActor) return;

        var current = _context.currentActor.value;
        var actor = current.Value;

        if (actor.actorState.value != ActorState.ACTED ||
            _context.isAnimating) return;

        actor.ReplaceActorState(ActorState.WAITING);
        _context.ReplaceCurrentActor(current.Next ?? current.List.First);
        Debug.Log("Actor " + actor + " has finished his turn");
    }
}