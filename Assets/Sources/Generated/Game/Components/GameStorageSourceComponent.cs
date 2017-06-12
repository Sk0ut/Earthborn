//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public StorageSourceComponent storageSource { get { return (StorageSourceComponent)GetComponent(GameComponentsLookup.StorageSource); } }
    public bool hasStorageSource { get { return HasComponent(GameComponentsLookup.StorageSource); } }

    public void AddStorageSource(GameEntity newValue) {
        var index = GameComponentsLookup.StorageSource;
        var component = CreateComponent<StorageSourceComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceStorageSource(GameEntity newValue) {
        var index = GameComponentsLookup.StorageSource;
        var component = CreateComponent<StorageSourceComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveStorageSource() {
        RemoveComponent(GameComponentsLookup.StorageSource);
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

    static Entitas.IMatcher<GameEntity> _matcherStorageSource;

    public static Entitas.IMatcher<GameEntity> StorageSource {
        get {
            if (_matcherStorageSource == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.StorageSource);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherStorageSource = matcher;
            }

            return _matcherStorageSource;
        }
    }
}