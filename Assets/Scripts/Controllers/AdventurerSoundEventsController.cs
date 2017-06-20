using System;
using System.Collections;
using System.Collections.Generic;
using Entitas.Unity;
using UnityEngine;

public class AdventurerSoundEventsController : MonoBehaviour
{

	private GameContext _game;

	public Action OnAttackEnd;
	public Action OnMeleeAttackHit;
	private GameEntity _entity;

	private void Start () {
		var contexts = Contexts.sharedInstance;
		_game = contexts.game;
		_entity = (GameEntity) gameObject.GetComponentInParent<EntityLink>().entity;
	}
	
	public void Footstep()
	{
		var evt = _game.CreateEvent(Event.Footstep);
		evt.AddTarget(_entity);
	}
	
	public void MeleeHit(int num)
	{
		if (OnAttackEnd != null) OnMeleeAttackHit();
	}
	public void EndAttack()
	{
		if (OnAttackEnd != null) OnAttackEnd();
		
		var evt = _game.CreateEvent(Event.MeleeAttack);
		evt.AddTarget(_entity);
	}
}
