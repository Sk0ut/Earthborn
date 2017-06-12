using System.Collections;
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class AnimationQueueComponent : IComponent
{
    public Queue<GameEntity> value;
}