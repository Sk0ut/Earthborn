//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public EasingComponent easing { get { return (EasingComponent)GetComponent(GameComponentsLookup.Easing); } }
    public bool hasEasing { get { return HasComponent(GameComponentsLookup.Easing); } }

    public void AddEasing(float newDuration) {
        var index = GameComponentsLookup.Easing;
        var component = CreateComponent<EasingComponent>(index);
        component.duration = newDuration;
        AddComponent(index, component);
    }

    public void ReplaceEasing(float newDuration) {
        var index = GameComponentsLookup.Easing;
        var component = CreateComponent<EasingComponent>(index);
        component.duration = newDuration;
        ReplaceComponent(index, component);
    }

    public void RemoveEasing() {
        RemoveComponent(GameComponentsLookup.Easing);
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

    static Entitas.IMatcher<GameEntity> _matcherEasing;

    public static Entitas.IMatcher<GameEntity> Easing {
        get {
            if (_matcherEasing == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Easing);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherEasing = matcher;
            }

            return _matcherEasing;
        }
    }
}