using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AnimationSystem : IInitializeSystem, IExecuteSystem, ICleanupSystem, ITearDownSystem
{
    private GameContext _game;
    private Queue<IEnumerator> _animations;
    private IEnumerator _current;
    private GameObject _controllerObject;
    private AnimationController _controller;
    
    public AnimationSystem(Contexts contexts)
    {
        _game = contexts.game;
    }

    public void Initialize()
    {
        _game.isAnimating = false;
        _game.SetAnimationQueue(new Queue<IEnumerator>());
        _animations = _game.animationQueue.value;
        _controllerObject = new GameObject("Animation Controller", typeof(AnimationController));
        _controller = _controllerObject.GetComponent<AnimationController>();
    }

    public void Execute()
    {
        if (!_controller.Running && _animations.Count > 1)
        {
            _controller.RunAnimations(_animations);
            _game.isAnimating = true;
        }
    }

    public void Cleanup()
    {
        if (!_controller.Running) _game.isAnimating = false;
    }

    public void TearDown()
    {
        Object.Destroy(_controllerObject);
        _controllerObject = null;
        _controller = null;
    }
}