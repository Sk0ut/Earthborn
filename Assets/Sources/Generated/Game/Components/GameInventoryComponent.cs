//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly InventoryComponent inventoryComponent = new InventoryComponent();

    public bool isInventory {
        get { return HasComponent(GameComponentsLookup.Inventory); }
        set {
            if (value != isInventory) {
                if (value) {
                    AddComponent(GameComponentsLookup.Inventory, inventoryComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.Inventory);
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

    static Entitas.IMatcher<GameEntity> _matcherInventory;

    public static Entitas.IMatcher<GameEntity> Inventory {
        get {
            if (_matcherInventory == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Inventory);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherInventory = matcher;
            }

            return _matcherInventory;
        }
    }
}
