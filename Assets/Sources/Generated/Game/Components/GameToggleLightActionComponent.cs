//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly ToggleLightActionComponent toggleLightActionComponent = new ToggleLightActionComponent();

    public bool isToggleLightAction {
        get { return HasComponent(GameComponentsLookup.ToggleLightAction); }
        set {
            if (value != isToggleLightAction) {
                if (value) {
                    AddComponent(GameComponentsLookup.ToggleLightAction, toggleLightActionComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.ToggleLightAction);
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

    static Entitas.IMatcher<GameEntity> _matcherToggleLightAction;

    public static Entitas.IMatcher<GameEntity> ToggleLightAction {
        get {
            if (_matcherToggleLightAction == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ToggleLightAction);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherToggleLightAction = matcher;
            }

            return _matcherToggleLightAction;
        }
    }
}