using System;
using UnityEngine;
using Entitas;

public class RandomDamageSystem	:  IExecuteSystem{

	private readonly GameContext _context;
	private IGroup<GameEntity> group;

	public RandomDamageSystem(Contexts contexts)
	{
		_context = contexts.game;

		group = _context.GetGroup (GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Health));
	}

	public void Execute()
	{
		System.Random rnd = new System.Random ();

		if (rnd.NextDouble () > 0.90f) {
			foreach (var e in group.GetEntities()) {
				e.ReplaceHealth(e.health.value-1);
			}
			Debug.Log ("Health decremented");
		}

	}
}


