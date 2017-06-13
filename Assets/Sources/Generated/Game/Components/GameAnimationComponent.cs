//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AnimationComponent animation { get { return (AnimationComponent)GetComponent(GameComponentsLookup.Animation); } }
    public bool hasAnimation { get { return HasComponent(GameComponentsLookup.Animation); } }

    public void AddAnimation(System.Collections.IEnumerator newValue) {
        var index = GameComponentsLookup.Animation;
        var component = CreateComponent<AnimationComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnimation(System.Collections.IEnumerator newValue) {
        var index = GameComponentsLookup.Animation;
        var component = CreateComponent<AnimationComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnimation() {
        RemoveComponent(GameComponentsLookup.Animation);
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

    static Entitas.IMatcher<GameEntity> _matcherAnimation;

    public static Entitas.IMatcher<GameEntity> Animation {
        get {
            if (_matcherAnimation == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Animation);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAnimation = matcher;
            }

            return _matcherAnimation;
        }
    }
}
