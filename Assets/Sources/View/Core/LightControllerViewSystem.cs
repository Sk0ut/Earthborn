using UnityEngine;
using Entitas;

public class LightControllerViewSystem : ReactiveSystem<GameEntity>
{
	public LightControllerViewSystem (Contexts contexts) : base(contexts.game)
	{
	}

	protected override ICollector<GameEntity> GetTrigger (IContext<GameEntity> context)
	{
		return context.CreateCollector (GameMatcher.AllOf(GameMatcher.LightSource, GameMatcher.View));
	}

	protected override bool Filter (GameEntity entity)
	{
		return entity.hasLightSource;
	}

	protected override void Execute (System.Collections.Generic.List<GameEntity> entities)
	{
		foreach (var entity in entities) {
			entity.view.gameObject.GetComponent<Light>().range = entity.lightSource.active ? 10f : 2.5f;
		}
	}
}