using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique, CreateAssetMenu]
public class Globals : ScriptableObject
{
    public float TileSize = 1f;
    public float EnergyThreshold = 1f;
	public int PlayerHealth = 100;
	public int PlayerLightingRadius = 5;
}