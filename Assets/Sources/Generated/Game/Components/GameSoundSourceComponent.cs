//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly SoundSourceComponent soundSourceComponent = new SoundSourceComponent();

    public bool isSoundSource {
        get { return HasComponent(GameComponentsLookup.SoundSource); }
        set {
            if (value != isSoundSource) {
                if (value) {
                    AddComponent(GameComponentsLookup.SoundSource, soundSourceComponent);
                } else {
                    RemoveComponent(GameComponentsLookup.SoundSource);
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

    static Entitas.IMatcher<GameEntity> _matcherSoundSource;

    public static Entitas.IMatcher<GameEntity> SoundSource {
        get {
            if (_matcherSoundSource == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.SoundSource);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSoundSource = matcher;
            }

            return _matcherSoundSource;
        }
    }
}
