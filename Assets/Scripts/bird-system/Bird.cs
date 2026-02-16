using System;
using System.Collections;
using System.Net.Http.Headers;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public string birdName;
    public float birdSpeed;
    
    public SpriteRenderer spriteRenderer;

    [SerializeField] bool _spawnedLeft;

    public float leftSpawn;
    public float rightSpawn;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(BirdData data)
    {
        birdSpeed = data.speed;
        birdName = data.birdName;
        print($"Initialized Bird Data {birdName}");
    }

    private void Start()
    {
        //Swaps the orientation of the sprite based on where it spawned
        switch (transform.position.x)
        {
            case < 0:
                spriteRenderer.flipX = false;
                _spawnedLeft = true;
                break;
            
            case > 0:
                spriteRenderer.flipX = true;
                _spawnedLeft = false;
                break;
        }
    }

    private void Update()
    {
        MoveAcross(_spawnedLeft);

        if (transform.position.x > rightSpawn + 1 || transform.position.x < leftSpawn - 1)
        {
            Destroy(gameObject);
        }
    }

    private void MoveAcross(bool left)
    {
        if (!left)
        {
            transform.Translate(Vector3.left * (Time.deltaTime * birdSpeed));
        }
        else
        {
            transform.Translate(Vector3.right * (Time.deltaTime * birdSpeed));
        }
    }
}
