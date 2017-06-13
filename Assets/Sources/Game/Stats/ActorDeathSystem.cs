using Entitas;
using Entitas.Unity;
using UnityEngine;

public class ActorDeathSystem : ReactiveSystem<GameEntity>
{
	public ActorDeathSystem (Contexts contexts) : base(contexts.game)
	{
		
	}

	#region implemented abstract members of ReactiveSystem

	protected override ICollector<GameEntity> GetTrigger (IContext<GameEntity> context)
	{
		return context.CreateCollector (GameMatcher.AllOf (GameMatcher.Actor, GameMatcher.Health).NoneOf(GameMatcher.Player, GameMatcher.Boss));
	}

	protected override bool Filter (GameEntity entity)
	{
		return entity.isActor && entity.hasHealth && entity.health.value <= 0;
	}

	// @TODO evento para dead e verificação especial para o Player
	protected override void Execute (System.Collections.Generic.List<GameEntity> entities)
	{
		foreach (var e in entities) {
			if (e.hasView) {
				e.view.gameObject.Unlink ();
				Object.Destroy (e.view.gameObject);
			}
			e.Destroy ();
		}
	}

	#endregion
}

