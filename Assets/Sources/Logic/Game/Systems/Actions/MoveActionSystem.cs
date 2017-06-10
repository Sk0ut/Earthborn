using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class MoveActionSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _context;
	private readonly IGroup<GameEntity> blockingGroup;

	public MoveActionSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
		blockingGroup = _context.GetGroup (GameMatcher.AllOf(GameMatcher.Blocking, GameMatcher.Position));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(
            GameMatcher.MoveAction,
            GameMatcher.Target
        ));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasMoveAction &&
               entity.hasTarget &&
               entity.target.value.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var action in entities)
        {
            var target = action.target.value;
            
            var pos = target.position;
			var x = pos.x;
			var y = pos.y;

            switch (action.moveAction.value)
            {
                case MoveDirection.Up:
                    ++y;
                    break;
                case MoveDirection.Down:
                    --y;
                    break;
                case MoveDirection.Left:
                    --x;
                    break;
                case MoveDirection.Right:
                    ++x;
                    break;
            }

	        var cancel = blockingGroup.GetEntities().Any(e => e.position.x == x && e.position.y == y);
	        
			/*
			bool cancelAction = false;
			foreach (var entity in blockingGroup.GetEntities()) {
				if (entity.position.x == x && entity.position.y == y) {
					cancelAction = true;
					break;
				}
			}*/

			if (!cancel) {
				target.ReplacePosition (x, y);
				target.ReplaceActorEnergy(target.actorEnergy.energy - 1f);
				_context.ReplaceTurnState(TurnState.EndTurn);
				var ev = _context.CreateEvent(Event.ActorWalked);
				ev.AddTarget (target);
				ev.AddMoveAction (action.moveAction.value);
			}
            // Handled
            action.Destroy();
        }
    }
}