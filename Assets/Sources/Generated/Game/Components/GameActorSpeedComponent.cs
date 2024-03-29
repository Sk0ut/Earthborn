//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ActorSpeedComponent actorSpeed { get { return (ActorSpeedComponent)GetComponent(GameComponentsLookup.ActorSpeed); } }
    public bool hasActorSpeed { get { return HasComponent(GameComponentsLookup.ActorSpeed); } }

    public void AddActorSpeed(float newSpeed) {
        var index = GameComponentsLookup.ActorSpeed;
        var component = CreateComponent<ActorSpeedComponent>(index);
        component.speed = newSpeed;
        AddComponent(index, component);
    }

    public void ReplaceActorSpeed(float newSpeed) {
        var index = GameComponentsLookup.ActorSpeed;
        var component = CreateComponent<ActorSpeedComponent>(index);
        component.speed = newSpeed;
        ReplaceComponent(index, component);
    }

    public void RemoveActorSpeed() {
        RemoveComponent(GameComponentsLookup.ActorSpeed);
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

    static Entitas.IMatcher<GameEntity> _matcherActorSpeed;

    public static Entitas.IMatcher<GameEntity> ActorSpeed {
        get {
            if (_matcherActorSpeed == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ActorSpeed);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherActorSpeed = matcher;
            }

            return _matcherActorSpeed;
        }
    }
}
