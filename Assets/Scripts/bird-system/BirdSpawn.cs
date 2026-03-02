using System;
using System.Collections;
using System.Collections.Generic;
using bird_system;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Random = System.Random;

[RequireComponent(typeof(BoundsBox))]
public class BirdSpawn : MonoBehaviour
{
    public struct YConstrains
    {
        public float min;
        public float max;
    }
    
    private BoundsBox _box;

    private Vector3 _spawnLeft;
    private Vector3 _spawnRight;
    private Vector3 _lastSpawnPos;
    
    private YConstrains _yConstrains;
    
    private bool _hasSpawnedBefore = false;
    private BirdData _lastSpawnedBird;

    private float _spawnTimer;
    [Tooltip("Time in seconds between spawning of birds.")]
    public float spawnTimerMax;

    [Tooltip("Chance from 0 - 1 that the spawn side will swap on next spawn.")]
    [Range(0f, 1f)] public float sideSwitchChance;
    
    [Header("Conditions")]
    [SerializeField]private BirdData.Biome currentBiome;
    [SerializeField]private BirdSpawnTable spawnTable;
    [SerializeField]private bool isNight;
    
    [HideInInspector]
    [Tooltip("Padding from the top and bottom of the spawn area.")] public float yPadding;
    [HideInInspector]
    [Tooltip("Minimum allowed distance between consecutive Y spawns.")] public float minYDistance = 1.5f;
    [HideInInspector]
    [Tooltip("Forced Y axis offset if spawn is too close.")] public float forcedYOffset = 2f;

    private void Awake()
    {
        _box = GetComponent<BoundsBox>();

        _spawnLeft = _box.WorldLeft;
        _spawnRight = _box.WorldRight;

        _yConstrains.max = _box.WorldTop.y - yPadding;
        _yConstrains.min = _box.WorldBottom.y + yPadding;
    }

    private void FixedUpdate()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= spawnTimerMax)
        {
            SpawnBird(GetSpawnPos());
            _spawnTimer = 0f;
        }
    }

    public void SpawnBird(Vector3 spawnPos)
    {
        BirdData data = spawnTable.GetRandomBird(currentBiome, isNight, _lastSpawnedBird);
        if (data == null) return;
        _lastSpawnedBird = data;
        
        GameObject birdObj = Instantiate(data.prefab, spawnPos, Quaternion.identity);
        BirdController birdController = birdObj.GetComponent<BirdController>();
        
        CreateBirdID(birdController, data);
        birdController.Initialize(data,_yConstrains);
        birdController.leftSpawn = _spawnLeft;
        birdController.rightSpawn = _spawnRight;
        birdObj.transform.SetParent(transform);
    }

    public Vector3 GetSpawnPos()
    {
        //First spawn where it decides the initial side and Y value to spawn a bird
        if (!_hasSpawnedBefore)
        {
            _lastSpawnPos = UnityEngine.Random.value < 0.5f ? _spawnLeft : _spawnRight;
            _lastSpawnPos.y = UnityEngine.Random.Range(_yConstrains.min, _yConstrains.max);
            _hasSpawnedBefore = true;
            return _lastSpawnPos;
        }

        //Decide whether to switch sides on next spawn
        if (UnityEngine.Random.value < sideSwitchChance)
        {
            _lastSpawnPos.x = _lastSpawnPos.x == _spawnLeft.x ? _spawnRight.x : _spawnLeft.x;
        }

        float newY = UnityEngine.Random.Range(_yConstrains.min, _yConstrains.max);

        //Check if the Y value of next spawn will be too close to previous Y
        if (Mathf.Abs(newY - _lastSpawnPos.y) < minYDistance)
        {
            //Push new spawn of Y up or down ( 50% chance ) 
            
            //bool to decide if the new spawn goes up or down
            bool goUp = UnityEngine.Random.value < 0.5f;

            if (goUp)
                newY = _lastSpawnPos.y + forcedYOffset;
            else
                newY = _lastSpawnPos.y - forcedYOffset;

            //Clamp the values to the bounds of the spawn area
            newY = Mathf.Clamp(newY, _yConstrains.min, _yConstrains.max);
        }

        _lastSpawnPos.y = newY;

        return _lastSpawnPos;
    }

    private void CreateBirdID(BirdController bC, BirdData data)
    {
        int randomIdNum = UnityEngine.Random.Range(100, 1000);
        bC.birdID = $"{data.birdID}{randomIdNum}";
    }
}
