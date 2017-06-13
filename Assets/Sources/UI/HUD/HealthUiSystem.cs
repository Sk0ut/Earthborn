using System.Collections.Generic;
using Entitas;
using TMPro;
using UnityEngine;

public class HealthUiSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private readonly Contexts _contexts;
    private UiEntity _ui;

    public HealthUiSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var prefab = Resources.Load<GameObject>("UI/PlayerHealth");
        _ui = _contexts.ui.CreateEntity();
        _ui.AddUi(Object.Instantiate(prefab));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Health);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isPlayer &&
               entity.hasHealth;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var player = entities.SingleEntity();
        _ui.ui.value.GetComponent<TextMeshProUGUI>().SetText(
            "<#ffff00>Sanity</color> <b>{0}/<#aaa>{1}</color></b>",
            player.health.value,
            player.health.max);
    }
}