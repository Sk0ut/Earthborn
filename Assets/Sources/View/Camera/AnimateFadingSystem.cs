using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;
using UnityEngine.Networking.Match;

public class AnimateFadingSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly Material _hiderMaterial;
    
    private Dictionary<GameEntity, Material> _originalMaterials;

    public AnimateFadingSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _hiderMaterial = Resources.Load<Material>("Materials/lol1");
        _originalMaterials = new Dictionary<GameEntity, Material>();
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Fade.AddedOrRemoved());
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView &&
               entity.view.gameObject.GetComponent<MeshRenderer>() != null;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var go = e.view.gameObject;
            var mr = go.GetComponent<MeshRenderer>();
            var mat = go.GetComponent<MeshRenderer>().material;
            
            if (e.isFade)
            {
                if (!_originalMaterials.ContainsKey(e))
                    _originalMaterials.Add(e, mat);
                mr.material = _hiderMaterial;
                mr.material.DOFade(0.5f, 0.5f)
                    .SetEase(Ease.InExpo);
            }
            else
            {
                var original = _originalMaterials[e];
                mat.DOFade(1f, 0.5f)
                    .SetEase(Ease.OutExpo)
                    .OnComplete(() =>
                    {
                        if (e.isFade) return;
                        mr.material = original;
                        _originalMaterials.Remove(e);
                    });
            }
        }
    }
}