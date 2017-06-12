using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class AnimateDroppedItemSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;

    public AnimateDroppedItemSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position.Added(), GameMatcher.StorageSource.Removed());
    }

    protected override bool Filter(GameEntity item)
    {
        return item.isItem &&
               item.hasView &&
               !item.hasStorageSource &&
               item.hasPosition;
    }

    protected override void Execute(List<GameEntity> droppedItems)
    {
        foreach (var item in droppedItems)
        {
            var gameObject = item.view.gameObject;
            gameObject.transform.DOKill();
            
            var pos = gameObject.transform.position;
            var up = gameObject.transform.DOMove(
                    pos + Vector3.up * 0.1f,
                    1.5f)
                .SetEase(Ease.OutExpo)
                .SetLoops(-1, LoopType.Yoyo)
                .Play();

            var rot = gameObject.transform.rotation.eulerAngles;
            gameObject.transform.DORotate(Vector3.up * 360f, 5f, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1)
                .Play();
        }
    }
}