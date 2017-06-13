using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class CurrentActorComponent : IComponent
{
    public LinkedListNode<GameEntity> value;
}