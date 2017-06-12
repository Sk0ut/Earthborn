using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class CurrentFloorComponent : IComponent
{
	public int value;
}
	