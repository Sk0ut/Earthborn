using Entitas;
using UnityEngine;

[Input]
public class MouseInputComponent : IComponent
{
    public int button;
    public Vector2 position;
}