using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class AnimatePlayerMoveSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _game;

    public AnimatePlayerMoveSystem(Contexts contexts) : base(contexts.game)
    {
        _game = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.EventType);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasEventType &&
               entity.eventType.value == Event.ActorWalked &&
               entity.hasTarget &&
               entity.hasMoveAction &&
               entity.target.value.hasView &&
               entity.target.value.view.gameObject.GetComponent<Animator>() != null;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var ev in entities)
        {
            var target = ev.target.value;

            var animator = target.view.gameObject.GetComponent<Animator>();
            var pos = target.position;
            var transform = target.view.gameObject.transform;
            var rot = transform.rotation.eulerAngles;

            var directionAngle = 0;
            switch (ev.moveAction.value)
            {
                case MoveDirection.Left:
                    directionAngle = -90;
                    break;
                case MoveDirection.Down:
                    directionAngle = 180;
                    break;
                case MoveDirection.Right:
                    directionAngle = 90;
                    break;
            }

            Debug.Log(rot);
            var rotate = transform.DORotate(
                new Vector3(rot.x, directionAngle, rot.z),
                0.3f);

            var move = transform.DOMove(
                new Vector3(pos.x, transform.position.y, pos.y),
                1f
            ).SetEase(Ease.InOutSine).Pause();

            move.OnStart(() => animator.SetBool("Walking", true));
            move.OnComplete(() => animator.SetBool("Walking", false));

            var seq = DOTween.Sequence();
            seq.Append(rotate)
                .Append(move);

            var animation = _game.CreateEntity();
            animation.AddAnimation(CreateAnimation(seq));
            
            // Handled
            ev.Destroy();
        }
    }

    IEnumerator CreateAnimation(Sequence seq)
    {
        var done = false;
        seq.OnStart(() =>
        {
            //Debug.Log("Move started " + DateTime.Now);
        });
        seq.OnComplete(() =>
        {
            //Debug.LogWarning("IM FUKIN DUNE " + DateTime.Now);
            done = true;
        });

        seq.Play();
        while (!done)
        {
            yield return null;
        }
    }
}