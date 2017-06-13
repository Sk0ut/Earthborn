using Entitas;
using System;

public class AIControlSystem : IExecuteSystem
{
	private readonly GameContext _gameContext;
	private readonly InputContext _inputContext;
	private readonly IGroup<GameEntity> _playerGroup;
	private Random _rnd;

	public AIControlSystem (Contexts contexts)
	{
		_gameContext = contexts.game;
		_inputContext = contexts.input;
		_playerGroup = _gameContext.GetGroup (GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Position));
		_rnd = new Random ();
	}
		
	public void Execute ()
	{
		var currentActor = _gameContext.GetCurrentActor ();
		if (!currentActor.hasAIControl) {
			return;
		}
		var command = _gameContext.CreateAction(currentActor);
		var x = currentActor.position.x;
		var y = currentActor.position.y;
		var player = _playerGroup.GetSingleEntity ();

		Direction? playerDirection = null;

		if (x == player.position.x) {
			if (y == player.position.y - 1) {
				playerDirection = Direction.Up;
			} else if (y == player.position.y + 1) {
				playerDirection = Direction.Down;
			}
		} else if (y == player.position.y) {
			if (x == player.position.x - 1) {
				playerDirection = Direction.Right;
			} else if (x == player.position.x + 1) {
				playerDirection = Direction.Left;
			}
		}

		if (playerDirection != null && _rnd.NextDouble() < _gameContext.globals.value.EnemyAttackProbability) {
			command.AddAttackAction ((Direction)playerDirection);
		} else {
			var moveDirectionValues = Enum.GetValues (typeof(Direction));
			command.AddMoveAction ((Direction)moveDirectionValues.GetValue (_rnd.Next (moveDirectionValues.Length)));
		}
	}
}

