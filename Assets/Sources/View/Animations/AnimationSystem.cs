using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AnimationSystem : IInitializeSystem, IExecuteSystem, ICleanupSystem
{
    private GameContext _game;
    private Queue<GameEntity> _animations;
    private GameEntity _current;
    private IGroup<GameEntity> _immediate;
    private IGroup<GameEntity> _all;
    
    public AnimationSystem(Contexts contexts)
    {
        _game = contexts.game;
    }

    public void Initialize()
    {
        _game.isAnimating = false;
        _game.SetAnimationQueue(new Queue<GameEntity>());
        _animations = _game.animationQueue.value;
        _immediate = _game.GetGroup(GameMatcher.AllOf(
            GameMatcher.Animation,
            GameMatcher.Immediate
        ));
        _all = _game.GetGroup(GameMatcher.Animation);
    }

    public void Execute()
    {
        if (_current != null)
        {
            if (!PlayAnimation(_current))
            {
                _current = null;
            }
        }
        if (_current == null)
        {
            _current = _animations.Count > 0 ? _animations.Dequeue() : null;

            if (_current != null)
            {
                //Debug.Log("Animation started");
            }
        }

        foreach (var immediate in _immediate.GetEntities())
        {
            PlayAnimation(immediate);
        }

        _game.isAnimating = _all.count > 0;
    }

    public void Cleanup()
    {
    }

    private bool PlayAnimation(GameEntity animationEntity)
    {
        if (!animationEntity.animation.value.MoveNext())
        {
            //Debug.Log("Animation finished");
            animationEntity.Destroy();
            return false;
        }
        return true;
    }
}