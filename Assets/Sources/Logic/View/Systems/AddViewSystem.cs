using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class AddViewSystem : ReactiveSystem<GameEntity>
{
    private readonly Transform _container = new GameObject("Views").transform;
    private readonly GameContext _context;

    public AddViewSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Asset);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasAsset && !entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var asset = e.asset.prefab;
            try
            {
                var gameObject = UnityEngine.Object.Instantiate(asset, _container, false);
                e.AddView(gameObject);
                gameObject.Link(e, _context);
            }
            catch (Exception ex)
            {
                Debug.LogError("Couldn't instantiate " + e.asset.prefab);
                Debug.LogException(ex);
            }
        }
    }
}