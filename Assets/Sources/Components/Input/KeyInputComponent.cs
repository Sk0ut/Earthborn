using Entitas;
using UnityEngine;

[Input]
public class KeyInputComponent : IComponent
{
    public KeyCode key;
    public float value;
}