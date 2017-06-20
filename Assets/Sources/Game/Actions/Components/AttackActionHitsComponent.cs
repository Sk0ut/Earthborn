using System.Collections.Generic;
using Entitas;

public struct HitData
{
    public float Damage;
    public GameEntity Target;

    public HitData(float damage, GameEntity target)
    {
        Damage = damage;
        Target = target;
    }
}
public class AttackActionHitsComponent : IComponent
{
    public List<HitData> value;
}