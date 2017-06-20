using System.Collections.Generic;
using Entitas;
using TMPro;
using UnityEngine;

public class OilUiSystem : ReactiveSystem<GameEntity>, IInitializeSystem
{
    private readonly Contexts _contexts;
    private UiEntity _ui;

    public OilUiSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var prefab = Resources.Load<GameObject>("UI/PlayerOil");
        _ui = _contexts.ui.CreateEntity();
        _ui.AddUi(Object.Instantiate(prefab));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Oil);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isPlayer &&
               entity.hasOil;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var player = entities.SingleEntity();
        _ui.ui.value.GetComponent<TextMeshProUGUI>().SetText(
            "<#aa6622>Oil</color> <b>{0}/<#aaa>{1}</color></b>",
            player.oil.value,
            player.oil.max);
    }
}