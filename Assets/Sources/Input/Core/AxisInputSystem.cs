using Entitas;
using UnityEngine;

public class AxisInputSystem : IExecuteSystem
{
    private InputContext _input;
    
    public AxisInputSystem(Contexts contexts)
    {
        _input = contexts.input;
    }
    
    public void Execute()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var xRaw = Input.GetAxisRaw("Horizontal");
        var yRaw = Input.GetAxisRaw("Vertical");

        var input = _input.CreateInput();
        input.AddAxisInput(new Vector2(x, y), new Vector2(xRaw, yRaw));
    }
}