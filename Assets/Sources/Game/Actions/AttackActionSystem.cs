using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class AttackActionSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _context;
	private readonly IGroup<GameEntity> _attackableEntitiesGroup;

    public AttackActionSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
		_attackableEntitiesGroup = _context.GetGroup (GameMatcher.AllOf (GameMatcher.Position, GameMatcher.Health));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(
            GameMatcher.AttackAction,
            GameMatcher.Target
        ));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasAttackAction &&
               entity.hasTarget &&
               entity.target.value.hasPosition &&
			entity.target.value.hasDamage;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var action in entities)
        {
            var target = action.target.value;
			var x = target.position.x;
			var y = target.position.y;

			switch (action.attackAction.direction) {
			case Direction.Up:
				++y;
				break;
			case Direction.Down:
				--y;
				break;
			case Direction.Left:
				--x;
				break;
			case Direction.Right:
				++x;
				break;
			}

            target.ReplaceActorEnergy(target.actorEnergy.energy - 1f);
            _context.ReplaceTurnState(TurnState.EndTurn);
            
            var ev = _context.CreateEvent(Event.ActorAttacked);
            ev.AddTarget(target);
            ev.AddPointing(action.attackAction.direction);



			foreach (var enemy in _attackableEntitiesGroup.GetEntities()) {
				if (enemy.position.x == x && enemy.position.y == y) {
					enemy.health.value -= target.damage.value;
					enemy.ReplaceComponent (GameComponentsLookup.Health, enemy.health);
				}
			}
            
            // Handled
            action.Destroy();
        }
    }
}