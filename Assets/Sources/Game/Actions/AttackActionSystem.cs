using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

public class AttackActionSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _context;

    public AttackActionSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
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

            target.ReplaceActorEnergy(target.actorEnergy.energy - 1f);
            _context.ReplaceTurnState(TurnState.EndTurn);
            
            var ev = _context.CreateEvent(Event.ActorAttacked);
            ev.AddTarget(target);
            ev.AddPointing(action.attackAction.direction);
            
            // Handled
            action.Destroy();
        }
    }
}