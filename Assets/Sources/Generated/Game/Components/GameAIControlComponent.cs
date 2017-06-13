//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AIControlComponent aIControl { get { return (AIControlComponent)GetComponent(GameComponentsLookup.AIControl); } }
    public bool hasAIControl { get { return HasComponent(GameComponentsLookup.AIControl); } }

    public void AddAIControl(AIControlType newType) {
        var index = GameComponentsLookup.AIControl;
        var component = CreateComponent<AIControlComponent>(index);
        component.type = newType;
        AddComponent(index, component);
    }

    public void ReplaceAIControl(AIControlType newType) {
        var index = GameComponentsLookup.AIControl;
        var component = CreateComponent<AIControlComponent>(index);
        component.type = newType;
        ReplaceComponent(index, component);
    }

    public void RemoveAIControl() {
        RemoveComponent(GameComponentsLookup.AIControl);
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

    static Entitas.IMatcher<GameEntity> _matcherAIControl;

    public static Entitas.IMatcher<GameEntity> AIControl {
        get {
            if (_matcherAIControl == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.AIControl);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAIControl = matcher;
            }

            return _matcherAIControl;
        }
    }
}
