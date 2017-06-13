using  Entitas;
using UnityEngine;

[Input]
public class AxisInputComponent : IComponent
{
    public Vector2 value;
    public Vector2 raw;
}