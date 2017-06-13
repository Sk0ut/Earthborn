public class SoundSystems : Feature
{
    public SoundSystems(Contexts contexts) : base("Sound Systems")
    {
        Add(new PickupItemSoundSystem(contexts));
        Add(new PlayerFootstepsSoundSystem(contexts));
    }
}