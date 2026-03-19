using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class BirdController : MonoBehaviour
{
    public string birdName;
    public float birdSpeed;
    public string birdID;
    
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    
    public bool isMoving;

    bool _spawnedLeft;

    [SerializeField] private bool _hasLanded;

    [HideInInspector] public Vector3 leftSpawn, rightSpawn;

    private Sprite _birdSit;
    private Sprite _birdFly;
    public BirdData birdData;
    private BirdBrain _brain;
    private BirdSpawn.YConstrains _yConstrains;
    public List<LandingSpotGroup> landingGroups = new List<LandingSpotGroup>();

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        _brain = GetComponent<BirdBrain>();
    }

    public void Initialize(BirdData data, BirdSpawn.YConstrains yConstrains)
    {
        birdSpeed = data.speed;
        birdName = data.birdName;
        _birdFly = spriteRenderer.sprite;
        _birdSit = data.birdSitSprite;
        birdData = data;
        _yConstrains = yConstrains;
        gameObject.name = $"{birdName} | ID: {birdID}";
        print($"Initialized Bird Data {birdName}");
        FlipSprite(transform.position.x);
        StartCoroutine(GetLandingGroups());
        StartCoroutine(BirdLife());
    }

    public void DespawnBird()
    {
        Destroy(gameObject);
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
    
    public IEnumerator FlyToCoroutine(Vector3 target, float speed)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                target,
                speed * Time.deltaTime);
            yield return null;
        }
    }
    
    private IEnumerator BirdLife()
    {
        yield return new WaitForSeconds(0.05f);
        while (gameObject.activeSelf)
        {
            if (!_hasLanded && _brain.WillLand())
            {
                var landSpot = _brain.ChooseNextLandingSpot(landingGroups, birdID, birdData.allowedLandingTypes);
                if (landSpot == null)
                {
                    Debug.Log($"Bird: {birdID} wanted to land but there were no spots.");
                    yield return null;
                    continue;
                }
                yield return StartCoroutine(FlyToCoroutine(landSpot.transform.position, birdSpeed));
                
                _hasLanded = true;
                isMoving = false;

                animator.enabled = false;
                SpriteChange();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                isMoving = true;
                animator.enabled = true;
                if (_brain.currentSpot != null)
                {
                    _brain.currentSpot.Release();
                    _brain.currentSpot = null;
                }
                Vector3 target;

                if (_spawnedLeft)
                {
                    //Fly off to the right, beyond spawn point to cause despawn
                    target = new Vector3(rightSpawn.x + 2f, 
                        UnityEngine.Random.Range(_yConstrains.min, _yConstrains.max), 
                        transform.position.z);
                }
                else
                {
                    //Fly off to the left, beyond spawn point to cause despawn
                    target = new Vector3(leftSpawn.x - 2f,
                        UnityEngine.Random.Range(_yConstrains.min, _yConstrains.max),
                        transform.position.z);
                }
                SpriteChange();
                yield return FlyToCoroutine(target, birdSpeed);
                
                DespawnBird();
                yield break;
            }

            yield return null;
        }
    }
    
    private IEnumerator GetLandingGroups()
    {
        yield return new WaitForSeconds(0.005f);
        Transform parent = transform.parent;
        landingGroups.AddRange(parent.GetComponentsInChildren<LandingSpotGroup>());
    }

    public void SpriteChange()
    {
        if (_birdSit != null)
        {
            if (isMoving)
            {
                spriteRenderer.sprite = _birdFly;
            }
            else
            {
                spriteRenderer.sprite = _birdSit;
            }
        }
    }
}
