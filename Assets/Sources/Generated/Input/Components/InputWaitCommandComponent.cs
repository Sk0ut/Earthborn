//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    static readonly WaitCommandComponent waitCommandComponent = new WaitCommandComponent();

    public bool isWaitCommand {
        get { return HasComponent(InputComponentsLookup.WaitCommand); }
        set {
            if (value != isWaitCommand) {
                if (value) {
                    AddComponent(InputComponentsLookup.WaitCommand, waitCommandComponent);
                } else {
                    RemoveComponent(InputComponentsLookup.WaitCommand);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherWaitCommand;

    public static Entitas.IMatcher<InputEntity> WaitCommand {
        get {
            if (_matcherWaitCommand == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.WaitCommand);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherWaitCommand = matcher;
            }

            return _matcherWaitCommand;
        }
    }
}
