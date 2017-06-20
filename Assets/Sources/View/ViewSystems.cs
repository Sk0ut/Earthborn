public class ViewSystems : Feature
{
    public ViewSystems(Contexts contexts) : base("View Systems")
    {
        Add(new InitializeViewSystem(contexts));
        
        Add(new RemoveViewSystem(contexts));
        Add(new AddViewSystem(contexts));
		Add (new AttachedViewSystem (contexts));
        Add(new SetViewPositionSystem(contexts));

		Add (new LightControllerViewSystem (contexts));
        
        // Camera
        Add(new SwitchCameraSystem(contexts));
        Add(new CameraFollowSystem(contexts));
        Add(new FadeObstructingObjectsSystem(contexts));
        
        // Items
        Add(new DrawDroppedItemSystem(contexts));
        Add(new AnimateDroppedItemSystem(contexts));
        Add(new DisablePickedItemViewSystem(contexts));

        // Animations
        Add(new AnimateFadingSystem(contexts));
        
        Add(new AnimateAdventurerMoveSystem(contexts));
        Add(new AnimateAdventurerAttackSystem(contexts));

        Add(new AnimateMinionMoveSystem(contexts));
        Add(new AnimateMinionAttackSystem(contexts));

        Add(new AnimateMinionBossMoveSystem(contexts));
        Add(new AnimateMinionBossAttackSystem(contexts));
        
        // Add(new AnimateMoveActionSystem(contexts));
        Add(new AnimateWaitActionSystem(contexts));

        Add(new AnimateRotationSystem(contexts));
        
        Add(new AddAnimationSystem(contexts));
        Add(new AnimationSystem(contexts));
    }
}