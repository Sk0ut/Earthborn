using Entitas;
using Entitas.CodeGeneration.Attributes;

[Input, Unique]
public class GameContextComponent : IComponent
{
    public bool enabled;
}