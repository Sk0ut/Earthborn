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
        //var key = Input.inputString;
        //var input = _input.CreateInput();
        //input.AddKeyInput(key, 1.0f);
    }
}