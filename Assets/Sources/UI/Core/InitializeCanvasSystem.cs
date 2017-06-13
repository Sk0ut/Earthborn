using Entitas;
using Entitas.Unity;
using UnityEngine;

public class InitializeCanvasSystem : IInitializeSystem
{
    private readonly Contexts _contexts;
    
    public InitializeCanvasSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var prefab = Resources.Load<GameObject>("UI/Canvas");
        var canvas = Object.Instantiate(prefab);
        _contexts.ui.SetCanvas(canvas);
    }
}