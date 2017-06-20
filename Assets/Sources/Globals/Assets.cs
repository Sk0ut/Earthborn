using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique, CreateAssetMenu]
public class Assets : ScriptableObject
{
    public GameObject Player;
    public GameObject Pistol;
    public GameObject GroundTile;
    public GameObject Wall;
    public GameObject Camera;
	public GameObject LightSource;
	public GameObject Enemy;
	public GameObject Boss;
	public GameObject Stairs;
	public GameObject HealthPotion;
	public GameObject OilPotion;
}