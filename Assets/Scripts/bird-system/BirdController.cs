using System;
using System.Collections;
using System.Net.Http.Headers;
using UnityEngine;

public class BirdController : MonoBehaviour
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
        FlipSprite(transform.position.x);
    }

    private void Update()
    {
        MoveAcross(_spawnedLeft);

        if (transform.position.x > rightSpawn + 1 || transform.position.x < leftSpawn - 1)
        {
            Destroy(gameObject);
        }
    }

    private void MoveAcross(bool left) //basic movement to opposite sides to allow for Demo [ BASIC SIDE TO SIDE ]
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
    
    private void FlipSprite(float xPos) //Swaps the orientation of the sprite based on which side it spawned
    {
        switch (xPos)
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
}
