using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ActorManagerSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _context;

    public ActorManagerSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
        _context.SetActors(new LinkedList<GameEntity>());
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Actor.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.isActor)
            {
                Debug.Log("New actor entered the loop: " + entity);
                var actors = _context.actors.value;
                var node = actors.AddLast(entity);
                _context.ReplaceActors(actors);

                if (actors.Count == 1)
                {
                    _context.SetCurrentActor(node);
                }
            }
            else if (!entity.isActor)
            {
                var actors = _context.actors.value;
                var current = _context.currentActor.value;

                if (!actors.Contains(entity)) continue;
                
                Debug.Log("Actor left the loop: " + entity);
                
                if (current.Value.Equals(entity))
                {
                    if (actors.Count == 1)
                    {
                        _context.RemoveCurrentActor();
                    }
                    else
                    {
                        _context.ReplaceCurrentActor(current.Next ?? current.List.First);
                    }
                }
                actors.Remove(entity);
                _context.ReplaceActors(actors);
            }
        }
    }
}