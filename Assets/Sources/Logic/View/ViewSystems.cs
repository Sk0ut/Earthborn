public class ViewSystems : Feature
{
    public ViewSystems(Contexts contexts) : base("View Systems")
    {
        Add(new InitializeViewSystem(contexts));
        
        Add(new RemoveViewSystem(contexts));
        Add(new AddViewSystem(contexts));
        Add(new SetViewPositionSystem(contexts));
        
        // Camera
        Add(new CameraSystem(contexts));
        Add(new FadeObstructingObjectsSystem(contexts));

        // Animations
        Add(new AnimateFadingSystem(contexts));
        Add(new AnimatePlayerMoveSystem(contexts));
        Add(new AnimateMoveActionSystem(contexts));
        Add(new AnimateWaitActionSystem(contexts));
        
        Add(new AddAnimationSystem(contexts));
        Add(new AnimationSystem(contexts));
    }
}