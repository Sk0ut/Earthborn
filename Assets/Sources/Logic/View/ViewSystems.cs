public class ViewSystems : Feature
{
    public ViewSystems(Contexts contexts) : base("View Systems")
    {
        Add(new InitializeViewSystem(contexts));
        
        Add(new RemoveViewSystem(contexts));
        Add(new AddViewSystem(contexts));
		Add (new AttachedViewSystem (contexts));
        Add(new SetViewPositionSystem(contexts));

        // Animations
        Add(new AnimatePlayerMoveSystem(contexts));
        Add(new AnimateMoveActionSystem(contexts));
        Add(new AnimateWaitActionSystem(contexts));
        
        Add(new AddAnimationSystem(contexts));
        Add(new AnimationSystem(contexts));
    }
}