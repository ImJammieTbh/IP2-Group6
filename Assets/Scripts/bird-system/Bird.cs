using System;
using System.Collections;
using System.Net.Http.Headers;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public string birdName;
    public float birdSpeed;
    
    public SpriteRenderer spriteRenderer;

    private bool _spawnedLeft;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    private void FlyTo()
    {
        
    }

    private IEnumerable FlyToCoroutine()
    {
        
        
        yield return null;
    }
}
