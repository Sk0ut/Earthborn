public class UISystems : Feature
{
    public UISystems(Contexts contexts) : base("UI Systems")
    {
        Add(new InitializeCanvasSystem(contexts));

        Add(new ItemPickupTooltipSystem(contexts));

        Add(new AddUiElementSystem(contexts));
    }
}