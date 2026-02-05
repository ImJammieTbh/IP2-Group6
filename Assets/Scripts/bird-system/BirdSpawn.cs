using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawn : MonoBehaviour
{
    private BoundsBox _box;

    private Vector3 _spawnLeft;
    private Vector3 _spawnRight;

    public GameObject debugCube;

    private void Awake()
    {
        _box = GetComponent<BoundsBox>();

        _spawnLeft = _box.WorldLeft;
        _spawnRight = _box.WorldRight;
    }

    private void Start()
    {
        SpawnBird(_spawnLeft);
        SpawnBird(_spawnRight);
    }

    public void SpawnBird(Vector3 spawnPos)
    {
        GameObject bird = Instantiate(debugCube, spawnPos, Quaternion.identity);
        bird.gameObject.name = $"Bird{spawnPos}";
        //StartCoroutine(MoveBird(spawnPos, bird));
    }

    public IEnumerator MoveBird(Vector3 spawnPos, GameObject bird)
    {
        bool moving = true;
        while (moving)
        {
            Vector3 pos = bird.transform.position;
            if (spawnPos != _spawnRight)
            {
                pos.x = Mathf.Lerp(pos.x, _spawnRight.x, Time.deltaTime * 1f);
                bird.transform.position = pos;
            }
            else if (spawnPos != _spawnLeft)
            {
                pos.x = Mathf.Lerp(pos.x, _spawnLeft.x, Time.deltaTime * 1f);
                bird.transform.position = pos;
            }
            else
            {
                moving = false;
            }
        }
        yield return null;
    }
}
