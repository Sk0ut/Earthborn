using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PlayerLowSanitySoundSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private AudioSource _audioSource;
    
    public PlayerLowSanitySoundSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        var go = new GameObject("LowSanitySound");
        _audioSource = go.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = true;
        _audioSource.clip = Resources.Load<AudioClip>("Sounds/eb_hero_lowSanity");
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Health);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasHealth &&
               entity.isPlayer == true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var ety in entities)
        {
            var percent = ety.health.value / ety.health.max;
            var severity = Mathf.Min(Mathf.Max(0, (0.3f - percent)) / 0.3f, 1);

            if (severity > 0)
            {
                _audioSource.volume = severity * 0.5f;
                if (!_audioSource.isPlaying)
                    _audioSource.Play();
            }
            else if (_audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
        }
    }
}