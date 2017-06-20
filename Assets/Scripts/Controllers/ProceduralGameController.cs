using Entitas;
using UnityEngine;

public class ProceduralGameController : MonoBehaviour
{
	public Globals Globals;
	public Assets Assets;

	private Systems _systems;

	private void Start()
	{
		var contexts = Contexts.sharedInstance;

		contexts.game.SetAssets(Assets);
		contexts.game.SetGlobals(Globals);

		_systems = CreateSystems(contexts);
		_systems.Initialize();
	}

	private void Update()
	{
		_systems.Execute();
		_systems.Cleanup();
	}

	private void OnDestroy()
	{
		_systems.TearDown();
	}

	private Systems CreateSystems(Contexts contexts)
	{
		return new Feature("Systems")
			.Add(new InputSystems(contexts))
			.Add(new ProceduralGameSystems(contexts))
			.Add(new ViewSystems(contexts))
			.Add(new UISystems(contexts))
			.Add(new SoundSystems(contexts));
	}
}