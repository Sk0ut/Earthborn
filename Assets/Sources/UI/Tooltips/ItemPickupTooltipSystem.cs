using System.Collections.Generic;
using System.Linq;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;
using UnityEngine;

public class ItemPickupTooltipSystem : IExecuteSystem
{
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _pickable;
    private readonly IGroup<GameEntity> _players;
    private readonly UiEntity _tooltip;

    public ItemPickupTooltipSystem(Contexts contexts)
    {
        _contexts = contexts;
        _pickable = contexts.game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Item,
                GameMatcher.Position)
            .NoneOf(GameMatcher.StorageSource));
        _players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Actor, GameMatcher.Player, GameMatcher.Position));
        _tooltip = contexts.ui.CreateEntity();
    }
    
    public void Execute()
    {
        foreach (var player in _players.GetEntities())
        {
            var pos = player.position;

            var canPickup = _pickable.GetEntities().Any(item => item.position.x == pos.x && item.position.y == pos.y);
            if (!canPickup)
            {
                if (_tooltip.hasUi)
                {
                    var go = _tooltip.ui.value;
                    go.SetActive(false);
                }
                return;
            };

            if (_tooltip.hasUi)
            {
                if (!_tooltip.ui.value.activeSelf)
                    _tooltip.ui.value.SetActive(true);
                return;
            }
            Debug.Log("CAN PICKUP ITEMS");
            var prefab = Resources.Load<GameObject>("UI/ItemPickupTooltip");
            _tooltip.AddUi(Object.Instantiate(prefab));
        }
    }
}