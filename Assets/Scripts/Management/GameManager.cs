using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoreController _scoreController;
    public PhotoManager photoManager;

    public bool birdsCaptured;
    public int numCorrectPhotos;

    public List<BirdData> targetBirds;
    private HashSet<BirdData> capturedBirds = new HashSet<BirdData>();
    
    public TMP_Text scoreText;
    
    public Dictionary<int, List<PhotoData>> birdPhotos =  new Dictionary<int, List<PhotoData>>();
    public bool photoReady;
    
    private float _tempPhotoScore = 0;
    
    public List<Transform> wantedBoardPositions = new List<Transform>();
    private GameObject[] taggedPos;
    public GameObject targetHint;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _scoreController = GetComponent<ScoreController>();
        
        CameraLag.OnPhotoTaken += CheckValidBird;
        PolaroidEjector.OnEjected += PhotoEjectedCall;
        
        taggedPos = GameObject.FindGameObjectsWithTag("targetBirdSlot");
        
        Init();
    }

    private void Start()
    {
        StartCoroutine(TargetsHintCoroutine());
    }

    private void FixedUpdate()
    {
        if (!birdsCaptured && capturedBirds.Count == targetBirds.Count)
        {
            birdsCaptured = true;
            print("Birds captured --- Level Complete");
        }
    }

    public void Init()
    {
        foreach (var obj in taggedPos)
        {
            if (!wantedBoardPositions.Contains(obj.transform))
            {
                wantedBoardPositions.Add(obj.transform);
            }
        }
        
        for (int i = 0; i < targetBirds.Count; i++)//Display target birds on board
        {
            GameObject displayBird = Instantiate(targetBirds[i].prefab, wantedBoardPositions[i].position, Quaternion.Euler(0,90,0));
            
            var bc = displayBird.GetComponent<BirdController>();
            var bb = displayBird.GetComponent<BirdBrain>();
            var anim = displayBird.GetComponent<Animator>();
            var box = displayBird.GetComponent<BoxCollider>();

            bc.enabled = bb.enabled = anim.enabled = box.enabled = false;
            
            switch (targetBirds[i].birdSize)//adjust scale per size of bird
            {
                case BirdData.BirdSize.Small:
                    displayBird.transform.localScale *= 0.5f;
                    break;
                
                case BirdData.BirdSize.Medium:
                    displayBird.transform.localScale *= 0.3f;
                    break;
                
                case BirdData.BirdSize.Large:
                    displayBird.transform.localScale *= 0.2f;
                    break;
            }
            
            displayBird.transform.SetParent(wantedBoardPositions[i]);
        }
    }

    public void CheckValidBird(BirdData shotBird, BirdController birdController)
    {
        if (targetBirds.Count == 0)//case if the list of birds is not set
        {
            print("Target birds are empty");
        }
        else
        {
            StartCoroutine(ProcessLatestPhotoCoroutine(shotBird, birdController));
        }
    }

    public void PhotoEjectedCall()
    {
        photoReady = true;
    }

    private IEnumerator ProcessLatestPhotoCoroutine(BirdData birdData, BirdController birdController)
    {
        yield return new WaitUntil(() => photoReady);
        
        PhotoData latestPhoto = photoManager.LatestPhoto;

        if (birdPhotos.ContainsKey(birdData.birdID))
        {
            birdPhotos[birdData.birdID].Add(latestPhoto);
            print($"PhotoData Added: {birdData.birdID}");
        }
        else
        {
            birdPhotos.Add(birdData.birdID, new List<PhotoData>{latestPhoto});
            print($"New PhotoData Added: {birdData.birdID}");
        }
        
        float tempScore = _scoreController.GetPhotoScore(birdController.spriteRenderer);
        
        latestPhoto.photoScore = tempScore;
        
        photoReady = false;
    }
    
    private IEnumerator TargetsHintCoroutine()
    {
        yield return new WaitForSeconds(2f);
        
        targetHint.SetActive(true);
        
        yield return new WaitForSeconds(10f);
        
        targetHint.SetActive(false);
    }
}
