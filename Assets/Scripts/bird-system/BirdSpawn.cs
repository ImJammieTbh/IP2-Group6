using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class BirdSpawn : MonoBehaviour
{
    private BoundsBox _box;

    private Vector3 _spawnLeft;
    private Vector3 _spawnRight;
    private Vector3 _lastSpawnPos;

    private float _minY;
    private float _maxY;
    
    [Tooltip("Padding from the top and bottom of the spawn area.")]
    public float yPadding;
    
    public GameObject debugObject;

    private float _spawnTimer;
    public float spawnTimerMax;

    [Tooltip("Chance from 0 - 1 that the spawn side will swap on next spawn.")]
    [Range(0f, 1f)] public float switchChance;

    private void Awake()
    {
        _box = GetComponent<BoundsBox>();

        _spawnLeft = _box.WorldLeft;
        _spawnRight = _box.WorldRight;

        _maxY = _box.WorldTop.y - yPadding;
        _minY = _box.WorldBottom.y + yPadding;
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
        GameObject bird = Instantiate(debugObject, spawnPos, Quaternion.identity);
        bird.gameObject.name = $"Bird{spawnPos}";
        bird.transform.SetParent(this.transform);
    }

    public Vector3 GetSpawnPos()
    {
        if (_lastSpawnPos == null)
        {
            _lastSpawnPos = UnityEngine.Random.Range(0f, 1f) < 0.5f ? _spawnLeft : _spawnRight;
            return _lastSpawnPos;
        }

        //chooses the side it spawns on
        if (UnityEngine.Random.value < switchChance)
        {
            _lastSpawnPos.x = _lastSpawnPos.x == _spawnLeft.x ? _spawnRight.x : _spawnLeft.x;
        }
        
        _lastSpawnPos.y = UnityEngine.Random.Range(_minY, _maxY);
        return _lastSpawnPos;
    }
}
