using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Ui, Unique]
public class CanvasComponent : IComponent
{
    public GameObject value;
}