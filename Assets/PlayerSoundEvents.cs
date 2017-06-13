using System.Collections;
using System.Collections.Generic;
using Entitas.Unity;
using UnityEngine;

public class PlayerSoundEvents : MonoBehaviour
{

	private GameContext _game;

	private void Start () {
		var contexts = Contexts.sharedInstance;
		_game = contexts.game;
	}
	
	public void Footstep()
	{
		var entity = (GameEntity) gameObject.GetComponentInParent<EntityLink>().entity;
		var evt = _game.CreateEvent(Event.Footstep);
		evt.AddTarget(entity);
	}
	
	public void EndAttack()
	{
		var entity = (GameEntity) gameObject.GetComponentInParent<EntityLink>().entity;
		var evt = _game.CreateEvent(Event.Footstep);
		evt.AddTarget(entity);
	}
}
