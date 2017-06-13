//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PositionComponent position { get { return (PositionComponent)GetComponent(GameComponentsLookup.Position); } }
    public bool hasPosition { get { return HasComponent(GameComponentsLookup.Position); } }

    public void AddPosition(int newX, int newY) {
        var index = GameComponentsLookup.Position;
        var component = CreateComponent<PositionComponent>(index);
        component.x = newX;
        component.y = newY;
        AddComponent(index, component);
    }

    public void ReplacePosition(int newX, int newY) {
        var index = GameComponentsLookup.Position;
        var component = CreateComponent<PositionComponent>(index);
        component.x = newX;
        component.y = newY;
        ReplaceComponent(index, component);
    }

    public void RemovePosition() {
        RemoveComponent(GameComponentsLookup.Position);
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

    static Entitas.IMatcher<GameEntity> _matcherPosition;

    public static Entitas.IMatcher<GameEntity> Position {
        get {
            if (_matcherPosition == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Position);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPosition = matcher;
            }

            return _matcherPosition;
        }
    }
}
