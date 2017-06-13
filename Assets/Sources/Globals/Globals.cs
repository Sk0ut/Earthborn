using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique, CreateAssetMenu]
public class Globals : ScriptableObject
{
    public float TileSize = 1f;
    public float EnergyThreshold = 1f;
	public int PlayerHealth = 100;
	public int PlayerLightingRadius = 5;
	public float HealthIncreasePerTurn = 1f;
	public float HealthDecreasePerTurn = 0.25f;
	public int PlayerDamage = 20;
	public int EnemyDamage = 10;
	public int EnemyHealth = 60;
	public int BossMultiplier = 2;
}