using Entitas;
using System;

public class AIControlSystem : IExecuteSystem
{
	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;
	private Random _rnd;

	public AIControlSystem (Contexts contexts)
	{
		_gameContext = contexts.game;
		_inputContext = contexts.input;
		_rnd = new Random ();
	}
		
	public void Execute ()
	{
		var currentActor = _gameContext.GetCurrentActor ();
		if (!currentActor.hasAIControl) {
			return;
		}
		var command = _gameContext.CreateAction(currentActor);
	
		var moveDirectionValues = Enum.GetValues (typeof(Direction));
		command.AddMoveAction((Direction) moveDirectionValues.GetValue(_rnd.Next(moveDirectionValues.Length)));
	}
}

