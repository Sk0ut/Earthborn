//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly ColliderComponent colliderComponent = new ColliderComponent();

    public bool isCollider {
        get { return HasComponent(GameComponentsLookup.Collider); }
        set {
            if (value != isCollider) {
                if (value) {
                    AddComponent(GameComponentsLookup.Collider, colliderComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.Collider);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherCollider;

    public static Entitas.IMatcher<GameEntity> Collider {
        get {
            if (_matcherCollider == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Collider);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCollider = matcher;
            }

            return _matcherCollider;
        }
    }
}