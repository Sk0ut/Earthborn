//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MaxHealthComponent maxHealth { get { return (MaxHealthComponent)GetComponent(GameComponentsLookup.MaxHealth); } }
    public bool hasMaxHealth { get { return HasComponent(GameComponentsLookup.MaxHealth); } }

    public void AddMaxHealth(int newValue) {
        var index = GameComponentsLookup.MaxHealth;
        var component = CreateComponent<MaxHealthComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMaxHealth(int newValue) {
        var index = GameComponentsLookup.MaxHealth;
        var component = CreateComponent<MaxHealthComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMaxHealth() {
        RemoveComponent(GameComponentsLookup.MaxHealth);
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

    static Entitas.IMatcher<GameEntity> _matcherMaxHealth;

    public static Entitas.IMatcher<GameEntity> MaxHealth {
        get {
            if (_matcherMaxHealth == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MaxHealth);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMaxHealth = matcher;
            }

            return _matcherMaxHealth;
        }
    }
}
