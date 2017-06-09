using System;
using Entitas;
using UnityEngine;

public class KeyboardInputSystem : IExecuteSystem
{
    private InputContext _input;
    
    public KeyboardInputSystem(Contexts contexts)
    {
        _input = contexts.input;
    }
    
    public void Execute()
    {
        foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                //Debug.Log("KeyCode down: " + kcode);
                var input = _input.CreateInput();
                input.AddKeyInput(kcode, 1.0f);
            }
        }
    }
}