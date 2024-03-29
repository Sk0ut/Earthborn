//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CameraFollowComponent cameraFollow { get { return (CameraFollowComponent)GetComponent(GameComponentsLookup.CameraFollow); } }
    public bool hasCameraFollow { get { return HasComponent(GameComponentsLookup.CameraFollow); } }

    public void AddCameraFollow(GameEntity newValue) {
        var index = GameComponentsLookup.CameraFollow;
        var component = CreateComponent<CameraFollowComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCameraFollow(GameEntity newValue) {
        var index = GameComponentsLookup.CameraFollow;
        var component = CreateComponent<CameraFollowComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCameraFollow() {
        RemoveComponent(GameComponentsLookup.CameraFollow);
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

    static Entitas.IMatcher<GameEntity> _matcherCameraFollow;

    public static Entitas.IMatcher<GameEntity> CameraFollow {
        get {
            if (_matcherCameraFollow == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CameraFollow);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCameraFollow = matcher;
            }

            return _matcherCameraFollow;
        }
    }
}
