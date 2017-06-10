using System.Collections.Generic;
using System.Linq;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class FadeObstructingObjectsSystem : IInitializeSystem, IExecuteSystem, ICleanupSystem
{
    private readonly Contexts _contexts;
    private readonly IGroup<GameEntity> _alwaysVisible;
    private readonly IGroup<GameEntity> _faded;
    private readonly HashSet<GameEntity> _obstructing;

    public FadeObstructingObjectsSystem(Contexts contexts)
    {
        _contexts = contexts;
        _alwaysVisible = contexts.game.GetGroup(GameMatcher.AllOf(
            GameMatcher.AlwaysVisible,
            GameMatcher.View
        ));
        _faded = contexts.game.GetGroup(GameMatcher.Fade);
        _obstructing = new HashSet<GameEntity>();
    }

    public void Initialize()
    {
    }

    public void Execute()
    {
        if (!_contexts.game.isCamera ||
            !_contexts.game.cameraEntity.hasView) return;

        var camera = _contexts.game.cameraEntity.view.gameObject;
        var transform = camera.transform;

        foreach (var visibleEty in GetVisibleEntities(camera.GetComponent<Camera>()))
        {   
            var target = visibleEty.view.gameObject;

            //Cast a ray from this object's transform the the watch target's transform.
            var hits = Physics.RaycastAll(
                transform.position,
                target.transform.position - transform.position,
                Vector3.Distance(target.transform.position, transform.position),
                LayerMask.GetMask("Obstruct")
            );

            //Loop through all overlapping objects and disable their mesh renderer
            if (hits.Length <= 0) continue;
            foreach (var hit in hits)
            {
                var collider = hit.collider;
                var colliderObj = hit.collider.gameObject;
                var colliderEntity = (GameEntity) colliderObj.GetEntityLink().entity;

                if (colliderEntity == null ||
                    !colliderEntity.isObstructable ||
                    colliderObj.transform == target ||
                    collider.transform.root == target)
                    continue;

                _obstructing.Add(colliderEntity);
                colliderEntity.isFade = true;
            }
        }
    }

    public void Cleanup()
    {
        foreach (var e in _faded.GetEntities())
        {
            if (!_obstructing.Contains(e))
                e.isFade = false;
        }
        _obstructing.Clear();
    }

    private List<GameEntity> GetVisibleEntities(Camera camera)
    {
        var visible = (from ety in _alwaysVisible.GetEntities()
            let screenPoint = camera.WorldToViewportPoint(ety.view.gameObject.transform.position)
            let onScreen = screenPoint.z > 0 &&
                           screenPoint.x > 0 &&
                           screenPoint.x < 1 &&
                           screenPoint.y > 0 &&
                           screenPoint.y < 1
            where true select ety).ToList();

        return visible;
    }
}