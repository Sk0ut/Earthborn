using System.Diagnostics;
using Entitas;

public class EndTurnSystem : ICleanupSystem
{
    private readonly GameContext _context;
    private Stopwatch _stopwatch;

    public EndTurnSystem(Contexts contexts)
    {
        _context = contexts.game;
        _stopwatch = new Stopwatch();
    }

    public void Cleanup()
    {
        if (_context.turnState.value != TurnState.EndTurn)
            return;

        if (_context.isAnimating) return;
        var current = _context.currentActor.value;
        _context.ReplaceCurrentActor(current.Next ?? current.List.First);
        _context.ReplaceTurnState(TurnState.StartTurn);
    }
}