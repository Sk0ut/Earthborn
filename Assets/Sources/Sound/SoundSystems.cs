public class SoundSystems : Feature
{
    public SoundSystems(Contexts contexts) : base("Sound Systems")
    {
        Add(new AmbientSoundSystem(contexts));
        Add(new PlayerLowSanitySoundSystem(contexts));

        Add(new PickupItemSoundSystem(contexts));
        Add(new AdventurerFootstepsSoundSystem(contexts));
        Add(new AdventurerToggleLightSoundSystem(contexts));
        Add(new AdventurerAttackSoundSystem(contexts));

        Add(new MinionAttackSoundSystem(contexts));

        Add(new MinionBossAttackSoundSystem(contexts));
        Add(new MinionBossHurtSoundSystem(contexts));
    }
}