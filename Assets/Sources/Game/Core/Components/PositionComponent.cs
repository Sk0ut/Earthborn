using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public class PositionComponent : IComponent
{
	public int x;
	public int y;
}