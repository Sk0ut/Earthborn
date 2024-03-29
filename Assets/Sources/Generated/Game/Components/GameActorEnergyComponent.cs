//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ActorEnergyComponent actorEnergy { get { return (ActorEnergyComponent)GetComponent(GameComponentsLookup.ActorEnergy); } }
    public bool hasActorEnergy { get { return HasComponent(GameComponentsLookup.ActorEnergy); } }

    public void AddActorEnergy(float newEnergy) {
        var index = GameComponentsLookup.ActorEnergy;
        var component = CreateComponent<ActorEnergyComponent>(index);
        component.energy = newEnergy;
        AddComponent(index, component);
    }

    public void ReplaceActorEnergy(float newEnergy) {
        var index = GameComponentsLookup.ActorEnergy;
        var component = CreateComponent<ActorEnergyComponent>(index);
        component.energy = newEnergy;
        ReplaceComponent(index, component);
    }

    public void RemoveActorEnergy() {
        RemoveComponent(GameComponentsLookup.ActorEnergy);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherActorEnergy;

    public static Entitas.IMatcher<GameEntity> ActorEnergy {
        get {
            if (_matcherActorEnergy == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ActorEnergy);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherActorEnergy = matcher;
            }

            return _matcherActorEnergy;
        }
    }
}
