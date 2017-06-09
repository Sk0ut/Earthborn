using Entitas;
using UnityEngine;

public class ClearInputSystem : ICleanupSystem
{
    private InputContext _context;
    private IGroup<InputEntity> _inputs;
    
    public ClearInputSystem(Contexts contexts)
    {
        _context = contexts.input;
        _inputs = _context.GetGroup(InputMatcher.AnyOf(
            InputMatcher.Input,
            InputMatcher.Command
        ));
    }

    public void Cleanup()
    {
        foreach (var i in _inputs.GetEntities())
        {
            i.Destroy();
        }
    }
}