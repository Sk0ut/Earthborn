using System;
using Entitas;

public class CoroutineSystem : IExecuteSystem
{
    private IGroup<GameEntity> _coroutines;
    public CoroutineSystem(Contexts contexts)
    {
        _coroutines = contexts.game.GetGroup(GameMatcher.Coroutine);
    }
    public void Execute()
    {
        foreach (var co in _coroutines.GetEntities())
        {
            if (!co.coroutine.value.MoveNext()) co.Destroy();
        }
    }
}