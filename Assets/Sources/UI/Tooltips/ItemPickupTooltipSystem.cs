using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;
using TMPro;
using UnityEngine;

public class ItemPickupTooltipSystem : IExecuteSystem
{
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _pickable;
    private readonly IGroup<GameEntity> _players;
    private readonly UiEntity _tooltip;
    private bool _exiting = false;

    public ItemPickupTooltipSystem(Contexts contexts)
    {
        _contexts = contexts;
        _pickable = contexts.game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Item,
                GameMatcher.Position)
            .NoneOf(GameMatcher.StorageSource));
        _players = contexts.game.GetGroup(
            GameMatcher.AllOf(GameMatcher.Actor, GameMatcher.Player, GameMatcher.Position));
        _tooltip = contexts.ui.CreateEntity();
    }

    public void Execute()
    {
        var player = _players.GetSingleEntity();

        var pos = player.position;

        var canPickup = _pickable.GetEntities().Any(item => item.position.x == pos.x && item.position.y == pos.y);
        if (!canPickup)
        {
            if (_tooltip.hasUi && _tooltip.ui.value.activeSelf && !_exiting)
            {
                AnimateExit(_tooltip.ui.value);
            }
            return;
        }

        var targetPos = Camera.main.WorldToScreenPoint(player.view.gameObject.transform.position) + Vector3.down * 30f;

        if (!_tooltip.hasUi)
        {
            var prefab = Resources.Load<GameObject>("UI/ItemPickupTooltip");
            _tooltip.AddUi(Object.Instantiate(prefab));
            _tooltip.ui.value.SetActive(false);
            var r = _tooltip.ui.value.GetComponent<RectTransform>();
            r.anchoredPosition3D = targetPos;
        }

        if (!_tooltip.ui.value.activeSelf || _exiting)
            AnimateEntry(_tooltip.ui.value);

        var rect = _tooltip.ui.value.GetComponent<RectTransform>();
        rect.anchoredPosition3D = rect.anchoredPosition3D + (targetPos - rect.anchoredPosition3D) / 100;
    }

    private void AnimateEntry(GameObject tooltip)
    {
        tooltip.SetActive(true);
        var rect = tooltip.GetComponent<RectTransform>();
        var text = tooltip.GetComponent<TextMeshProUGUI>();
        
        rect.localScale = Vector3.one * 1.3f;
        var scaleTween = rect.DOScale(Vector3.one, 1f)
            .SetEase(Ease.OutCubic);

        text.alpha = 0;
        var alphaTween = DOTween.To(() => text.alpha, a => text.alpha = a, 1, 0.5f);
    }

    private void AnimateExit(GameObject tooltip)
    {
        _exiting = true;
        var rect = tooltip.GetComponent<RectTransform>();
        var text = tooltip.GetComponent<TextMeshProUGUI>();

        var scaleTween = rect.DOScale(Vector3.zero, 1f)
            .SetEase(Ease.InCubic)
            .OnComplete(() =>
            {
                _exiting = false;
                tooltip.SetActive(false);
            });

        var alphaTween = DOTween.To(() => text.alpha, a => text.alpha = a, 0, 0.5f)
            .SetEase(Ease.InExpo);
    }
}