using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique, CreateAssetMenu]
public class Assets : ScriptableObject
{
    public GameObject Player;
    public GameObject GroundTile;
    public GameObject Wall;
}