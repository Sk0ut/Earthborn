using Entitas;
using Entitas.Unity;
using UnityEngine;

public class BossDeathSystem : ReactiveSystem<GameEntity>
{
	public BossDeathSystem (Contexts contexts) : base(contexts.game)
	{

	}

	#region implemented abstract members of ReactiveSystem

	protected override ICollector<GameEntity> GetTrigger (IContext<GameEntity> context)
	{
		return context.CreateCollector (GameMatcher.AllOf (GameMatcher.Boss, GameMatcher.Health));
	}

	protected override bool Filter (GameEntity entity)
	{
		return entity.isBoss && entity.hasHealth && entity.health.value <= 0;
	}

	// @TODO aplicar evento e efetuar melhor terminação / transitar para menu principal
	protected override void Execute (System.Collections.Generic.List<GameEntity> entities)
	{
		Debug.Log ("Boss death");
		// Only observable if game is built, nothing done on editor testing
		Application.Quit ();
	}

	#endregion
}

