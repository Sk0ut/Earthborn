using UnityEngine;

public static class InputExtensions
{
	public static InputEntity CreateInput(this InputContext context)
	{
		var input = context.CreateEntity();
		input.isInput = true;
		
		return input;
	}
	
	public static InputEntity CreateCommand(this InputContext context)
	{
		var command = context.CreateEntity();
		command.isCommand = true;
		
		return command;
	}
}