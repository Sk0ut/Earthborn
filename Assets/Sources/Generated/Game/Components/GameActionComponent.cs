//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly ActionComponent actionComponent = new ActionComponent();

    public bool isAction {
        get { return HasComponent(GameComponentsLookup.Action); }
        set {
            if (value != isAction) {
                if (value) {
                    AddComponent(GameComponentsLookup.Action, actionComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.Action);
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

    static Entitas.IMatcher<GameEntity> _matcherAction;

    public static Entitas.IMatcher<GameEntity> Action {
        get {
            if (_matcherAction == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Action);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAction = matcher;
            }

            return _matcherAction;
        }
    }
}
