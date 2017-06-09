using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class GameContextSystem : ReactiveSystem<InputEntity>
{
    private readonly InputContext _input;

    public GameContextSystem(Contexts contexts) : base(contexts.input)
    {
        _input = contexts.input;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.AnyOf(
            InputMatcher.AxisInput,
            InputMatcher.KeyInput
        ));
    }

    protected override bool Filter(InputEntity entity)
    {
        return _input.hasGameContext && _input.gameContext.enabled &&
               (entity.hasAxisInput || entity.hasKeyInput);
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var input in entities)
        {
            if (input.hasAxisInput)
            {
                var raw = input.axisInput.raw;
                if (raw.y >= 1)
                {
                    var command = _input.CreateCommand();
                    command.AddMove(MoveDirection.Up);
                }
                else if (raw.y <= -1)
                {
                    var command = _input.CreateCommand();
                    command.AddMove(MoveDirection.Down);
                }
                else if (raw.x >= 1)
                {
                    var command = _input.CreateCommand();
                    command.AddMove(MoveDirection.Right);
                }
                else if (raw.x <= -1)
                {
                    var command = _input.CreateCommand();
                    command.AddMove(MoveDirection.Left);
                }

                // Input handled
                input.Destroy();
            }

            if (input.hasKeyInput)
            {
                switch (input.keyInput.key)
                {
                    case KeyCode.R:
                        _input.CreateCommand().isWaitCommand = true;
                        input.Destroy();
                        break;
                }
                
            }
        }
    }
}