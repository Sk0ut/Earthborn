using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class ItemPickupNotificationSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _players;
    private readonly IGroup<UiEntity> _notifications;

    public ItemPickupNotificationSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _players = contexts.game.GetGroup(
            GameMatcher.AllOf(GameMatcher.Actor, GameMatcher.Player, GameMatcher.Position));

        _notifications = contexts.ui.GetGroup(UiMatcher.AllOf(UiMatcher.Ui, UiMatcher.Notification));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.PickupItemEvent);
    }

    protected override bool Filter(GameEntity evt)
    {
        return evt.hasEventType &&
               evt.hasPickupItemEvent &&
               evt.pickupItemEvent.picker.Equals(_players.GetSingleEntity());
    }

    protected override void Execute(List<GameEntity> events)
    {
        foreach (var evt in events)
        {
            var item = evt.pickupItemEvent.item;
            var notifications = _notifications.count;

            var prefab = Resources.Load<GameObject>("UI/ItemPickupNotification");
            var notification = _contexts.ui.CreateEntity();
            notification.isNotification = true;
            notification.AddUi(Object.Instantiate(prefab));
            AnimateEntry(notification, item);
        }
    }

    private void AnimateEntry(UiEntity notification, GameEntity item)
    {
        var go = notification.ui.value;

        var rect = go.GetComponent<RectTransform>();
        var text = go.GetComponent<TextMeshProUGUI>();
        
        text.SetText(string.Format("Picked up <#8fs>{0}</color>", item.itemStats.name));

        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, (_notifications.count - 1) * 30);
        rect.DOAnchorPosY(_notifications.count * 30, 0.5f)
            .SetEase(Ease.OutBack);
        
        text.alpha = 0;
        var alphaTween = DOTween.To(() => text.alpha, a => text.alpha = a, 1, 0.5f);

        DOTween.Sequence().PrependInterval(4f).OnComplete(() => AnimateExit(notification)).Play();
    }

    private void AnimateExit(UiEntity notification)
    {
        var go = notification.ui.value;
        
        var text = go.GetComponent<TextMeshProUGUI>();

        DOTween.To(() => text.alpha, a => text.alpha = a, 0, 1f)
            .SetEase(Ease.InExpo)
            .OnComplete(() => notification.isDestroyed = true);
    }
}