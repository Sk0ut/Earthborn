//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly UsableItemComponent usableItemComponent = new UsableItemComponent();

    public bool isUsableItem {
        get { return HasComponent(GameComponentsLookup.UsableItem); }
        set {
            if (value != isUsableItem) {
                if (value) {
                    AddComponent(GameComponentsLookup.UsableItem, usableItemComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.UsableItem);
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

    static Entitas.IMatcher<GameEntity> _matcherUsableItem;

    public static Entitas.IMatcher<GameEntity> UsableItem {
        get {
            if (_matcherUsableItem == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.UsableItem);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherUsableItem = matcher;
            }

            return _matcherUsableItem;
        }
    }
}
