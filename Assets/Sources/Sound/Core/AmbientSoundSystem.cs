using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AmbientSoundSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private AudioSource _audioSource;
    private int _currentLevel = -1;

    public AmbientSoundSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        var go = new GameObject("AmbientSound");
        _audioSource = go.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = true;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CurrentFloor);
    }

    protected override bool Filter(GameEntity ev)
    {
        return ev.hasCurrentFloor;
    }

    protected override void Execute(List<GameEntity> events)
    {
        var newLevel = events.SingleEntity().currentFloor.value;
        Debug.Log("Oiii " + newLevel + " " + _currentLevel);

        if (newLevel >= 0 && newLevel < 2 && !(_currentLevel >= 0 && _currentLevel < 2))
        {
            _currentLevel = newLevel;
            _audioSource.clip = Resources.Load<AudioClip>("Sounds/eb_mission_1_ambient");
            _audioSource.Play();
        }
        else if (newLevel == 2 && _currentLevel != 2)
        {
            _currentLevel = newLevel;
            _audioSource.clip = Resources.Load<AudioClip>("Sounds/eb_boss_presence");
            _audioSource.Play();
        }
    }
}