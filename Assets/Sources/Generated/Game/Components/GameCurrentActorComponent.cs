//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity currentActorEntity { get { return GetGroup(GameMatcher.CurrentActor).GetSingleEntity(); } }
    public CurrentActorComponent currentActor { get { return currentActorEntity.currentActor; } }
    public bool hasCurrentActor { get { return currentActorEntity != null; } }

    public GameEntity SetCurrentActor(System.Collections.Generic.LinkedListNode<GameEntity> newValue) {
        if (hasCurrentActor) {
            throw new Entitas.EntitasException("Could not set CurrentActor!\n" + this + " already has an entity with CurrentActorComponent!",
                "You should check if the context already has a currentActorEntity before setting it or use context.ReplaceCurrentActor().");
        }
        var entity = CreateEntity();
        entity.AddCurrentActor(newValue);
        return entity;
    }

    public void ReplaceCurrentActor(System.Collections.Generic.LinkedListNode<GameEntity> newValue) {
        var entity = currentActorEntity;
        if (entity == null) {
            entity = SetCurrentActor(newValue);
        } else {
            entity.ReplaceCurrentActor(newValue);
        }
    }

    public void RemoveCurrentActor() {
        currentActorEntity.Destroy();
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

    public CurrentActorComponent currentActor { get { return (CurrentActorComponent)GetComponent(GameComponentsLookup.CurrentActor); } }
    public bool hasCurrentActor { get { return HasComponent(GameComponentsLookup.CurrentActor); } }

    public void AddCurrentActor(System.Collections.Generic.LinkedListNode<GameEntity> newValue) {
        var index = GameComponentsLookup.CurrentActor;
        var component = CreateComponent<CurrentActorComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCurrentActor(System.Collections.Generic.LinkedListNode<GameEntity> newValue) {
        var index = GameComponentsLookup.CurrentActor;
        var component = CreateComponent<CurrentActorComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCurrentActor() {
        RemoveComponent(GameComponentsLookup.CurrentActor);
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

    static Entitas.IMatcher<GameEntity> _matcherCurrentActor;

    public static Entitas.IMatcher<GameEntity> CurrentActor {
        get {
            if (_matcherCurrentActor == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CurrentActor);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCurrentActor = matcher;
            }

            return _matcherCurrentActor;
        }
    }
}
