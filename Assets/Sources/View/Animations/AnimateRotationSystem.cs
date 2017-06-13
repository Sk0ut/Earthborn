using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class AnimateRotationSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    
    public AnimateRotationSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Pointing);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPointing &&
               entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var ety in entities)
        {
            var transform = ety.view.gameObject.transform;
            var rot = transform.rotation.eulerAngles;
            
            var rotate = transform.DORotate(
                new Vector3(rot.x, GameExtensions.GetDirectionAngle(ety.pointing.direction), rot.z),
                0.2f
            ).Pause();
            
            var animation = _contexts.game.CreateEntity();
            animation.AddAnimation(CreateAnimation(rotate));
            animation.AddAnimationTarget(ety);
            animation.isImmediate = true;
        }
    }
    
    IEnumerator CreateAnimation(Tweener seq)
    {
        var done = false;
        seq.OnStart(() =>
        {
            //Debug.Log("Move started " + DateTime.Now);
        });
        seq.OnComplete(() =>
        {
            done = true;
        });

        seq.Play();
        while (!done)
        {
            yield return null;
        }
    }
}