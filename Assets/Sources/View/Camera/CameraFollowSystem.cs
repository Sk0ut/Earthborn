using Entitas;
using UnityEngine;

public class CameraFollowSystem : IExecuteSystem
{
    private readonly Contexts _contexts;
    
    public CameraFollowSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        if (!_contexts.game.cameraEntity.hasView ||
            !_contexts.game.cameraEntity.hasCameraFollow)
            return;

        var target = _contexts.game.cameraEntity.cameraFollow.value;
        if (!target.hasView) return;

        var camera = _contexts.game.cameraEntity.view.gameObject;
        var cameraPos = camera.transform.position;
        var targetPos = target.view.gameObject.transform.position + new Vector3(0f, 3f, -3f);

        var distance = targetPos - cameraPos;

        camera.transform.position = cameraPos + distance / 100;
    }
}