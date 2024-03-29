//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity assetsEntity { get { return GetGroup(GameMatcher.Assets).GetSingleEntity(); } }
    public AssetsComponent assets { get { return assetsEntity.assets; } }
    public bool hasAssets { get { return assetsEntity != null; } }

    public GameEntity SetAssets(Assets newValue) {
        if (hasAssets) {
            throw new Entitas.EntitasException("Could not set Assets!\n" + this + " already has an entity with AssetsComponent!",
                "You should check if the context already has a assetsEntity before setting it or use context.ReplaceAssets().");
        }
        var entity = CreateEntity();
        entity.AddAssets(newValue);
        return entity;
    }

    public void ReplaceAssets(Assets newValue) {
        var entity = assetsEntity;
        if (entity == null) {
            entity = SetAssets(newValue);
        } else {
            entity.ReplaceAssets(newValue);
        }
    }

    public void RemoveAssets() {
        assetsEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AssetsComponent assets { get { return (AssetsComponent)GetComponent(GameComponentsLookup.Assets); } }
    public bool hasAssets { get { return HasComponent(GameComponentsLookup.Assets); } }

    public void AddAssets(Assets newValue) {
        var index = GameComponentsLookup.Assets;
        var component = CreateComponent<AssetsComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAssets(Assets newValue) {
        var index = GameComponentsLookup.Assets;
        var component = CreateComponent<AssetsComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAssets() {
        RemoveComponent(GameComponentsLookup.Assets);
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

    static Entitas.IMatcher<GameEntity> _matcherAssets;

    public static Entitas.IMatcher<GameEntity> Assets {
        get {
            if (_matcherAssets == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Assets);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAssets = matcher;
            }

            return _matcherAssets;
        }
    }
}
