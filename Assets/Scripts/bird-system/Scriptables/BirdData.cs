using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bird System/BirdData")]
public class BirdData : ScriptableObject
{
    public enum Biome
    {
        Debugging,
        Biome1,
        Biome2,
        Biome3
    }
    
    [Header("Information")]
    public string birdName;
    public string birdDescription;
    public int birdID;
    
    [Header("Prefab")]
    public GameObject prefab;
    
    [Header("Spawn Configuration")]
    public float spawnWeight;
    public float speed;
    
    [Header("Conditions")]
    public Biome biome;
    public bool isNightOnly;
}
