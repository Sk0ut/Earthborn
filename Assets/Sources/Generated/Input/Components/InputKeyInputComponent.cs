//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public KeyInputComponent keyInput { get { return (KeyInputComponent)GetComponent(InputComponentsLookup.KeyInput); } }
    public bool hasKeyInput { get { return HasComponent(InputComponentsLookup.KeyInput); } }

    public void AddKeyInput(string newKey, float newValue) {
        var index = InputComponentsLookup.KeyInput;
        var component = CreateComponent<KeyInputComponent>(index);
        component.key = newKey;
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceKeyInput(string newKey, float newValue) {
        var index = InputComponentsLookup.KeyInput;
        var component = CreateComponent<KeyInputComponent>(index);
        component.key = newKey;
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveKeyInput() {
        RemoveComponent(InputComponentsLookup.KeyInput);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherKeyInput;

    public static Entitas.IMatcher<InputEntity> KeyInput {
        get {
            if (_matcherKeyInput == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.KeyInput);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherKeyInput = matcher;
            }

            return _matcherKeyInput;
        }
    }
}
