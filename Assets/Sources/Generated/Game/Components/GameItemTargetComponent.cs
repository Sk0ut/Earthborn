//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ItemTargetComponent itemTarget { get { return (ItemTargetComponent)GetComponent(GameComponentsLookup.ItemTarget); } }
    public bool hasItemTarget { get { return HasComponent(GameComponentsLookup.ItemTarget); } }

    public void AddItemTarget(GameEntity newValue) {
        var index = GameComponentsLookup.ItemTarget;
        var component = CreateComponent<ItemTargetComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceItemTarget(GameEntity newValue) {
        var index = GameComponentsLookup.ItemTarget;
        var component = CreateComponent<ItemTargetComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveItemTarget() {
        RemoveComponent(GameComponentsLookup.ItemTarget);
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

    static Entitas.IMatcher<GameEntity> _matcherItemTarget;

    public static Entitas.IMatcher<GameEntity> ItemTarget {
        get {
            if (_matcherItemTarget == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ItemTarget);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherItemTarget = matcher;
            }

            return _matcherItemTarget;
        }
    }
}
