using System;
using System.Collections.Generic;
using System.Diagnostics;
using Entitas;
using UniRx.Examples;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

        /*if (!_stopwatch.IsRunning)
        {
            _stopwatch.Reset();
            _stopwatch.Start();
            Debug.Log("End turn start");
        }

        Debug.Log(_stopwatch.ElapsedMilliseconds);
        if (_stopwatch.ElapsedMilliseconds >= 1000)
        {
            _stopwatch.Stop();
            Debug.LogError("END turn end");
            var current = _context.currentActor.value;
            _context.ReplaceCurrentActor(current.Next ?? current.List.First);
            Debug.Log("Actor " + current.Value + " has finished his turn");
            _context.ReplaceTurnState(TurnState.StartTurn);
        }*/
    }
}