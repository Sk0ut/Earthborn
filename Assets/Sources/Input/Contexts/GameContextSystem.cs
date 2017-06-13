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
                    command.AddMove(Direction.Up);
                }
                else if (raw.y <= -1)
                {
                    var command = _input.CreateCommand();
                    command.AddMove(Direction.Down);
                }
                else if (raw.x >= 1)
                {
                    var command = _input.CreateCommand();
                    command.AddMove(Direction.Right);
                }
                else if (raw.x <= -1)
                {
                    var command = _input.CreateCommand();
                    command.AddMove(Direction.Left);
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
                    case KeyCode.E:
                        _input.CreateCommand().isPickupItemCommand = true;
                        input.Destroy();
                        break;
                    case KeyCode.L:
                        _input.CreateCommand().isToggleLightCommand = true;
                        input.Destroy();
                        break;
				case KeyCode.K:
					_input.CreateCommand ().isFloorTransictionCommand = true;
					input.Destroy ();
					break;
                    case KeyCode.Space:
                        _input.CreateCommand().isAttackCommand = true;
                        input.Destroy();
                        break;
                }
            }
        }
    }
}