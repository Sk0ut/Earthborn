using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
class SetViewPositionSystem : ReactiveSystem<GameEntity>
{
    public SetViewPositionSystem(Contexts contexts) : base(contexts.game)
    {
    }
    
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.View);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var pos = e.position;
			e.view.gameObject.transform.position = new Vector3(pos.x, e.view.gameObject.transform.position.y, pos.y);
        }
    }
}