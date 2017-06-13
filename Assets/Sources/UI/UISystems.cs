public class UISystems : Feature
{
    public UISystems(Contexts contexts) : base("UI Systems")
    {
        Add(new InitializeCanvasSystem(contexts));

        Add(new HealthUiSystem(contexts));
        Add(new ItemPickupTooltipSystem(contexts));
        Add(new ItemPickupNotificationSystem(contexts));

        Add(new AddUiElementSystem(contexts));
        Add(new DestroyUiElementSystem(contexts));
    }
}