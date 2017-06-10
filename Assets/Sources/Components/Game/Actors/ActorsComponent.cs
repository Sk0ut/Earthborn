using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class ActorsComponent : IComponent
{
    public LinkedList<GameEntity> value;
}