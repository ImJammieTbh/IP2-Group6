using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using bird_system;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoreController _scoreController;
    public PhotoManager photoManager;

    public bool birdsCaptured;
    public List<BirdData> targetBirds;
    private HashSet<BirdData> capturedBirds = new HashSet<BirdData>();
    
    public Dictionary<int, List<PhotoData>> birdPhotos =  new Dictionary<int, List<PhotoData>>();
    private bool photoReady;
    
    private float _tempPhotoScore = 0;
    
    public List<Transform> wantedBoardPositions = new List<Transform>();
    private GameObject[] taggedPos;
    public GameObject targetHint;

    private BirdSpawnTable _birdSpawnTable;

    // public List<BirdData> tempList;
    
    [Header("End of day Config")]
    public GameObject endDay;
    public List<GameObject> starVersions;
    public List<Transform> polaroidPositions;
    public GameObject polaroidPrefab;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _scoreController = GetComponent<ScoreController>();
        
        CameraLag.OnPhotoTaken += CheckValidBird;
        PolaroidEjector.OnEjected += PhotoEjectedCall;
        
        CameraLag.OnPhotosUsed += EndDay;
        
        taggedPos = GameObject.FindGameObjectsWithTag("targetBirdSlot");
        
        Init();
    }

    private void Start()
    {
        //StartCoroutine(TargetsHintCoroutine());
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
        // tempList = _birdSpawnTable.GetRandomBirdsNoWeight(3);
        
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
        print("Processing PhotoData");
        yield return new WaitUntil(() => photoReady);
        
        PhotoData latestPhoto = photoManager.LatestPhoto;

        if (birdPhotos.ContainsKey(birdData.birdID))
        {
            birdPhotos[birdData.birdID].Add(latestPhoto);
            print($"PhotoData Added: {birdData.birdID}");
            _scoreController.AddScore(false, birdController.isMoving);
        }
        else
        {
            birdPhotos.Add(birdData.birdID, new List<PhotoData>{latestPhoto});
            print($"New PhotoData Added: {birdData.birdID}");
            if (targetBirds.Contains(birdData))
            {
                _scoreController.AddScore(true, birdController.isMoving);
            }
            else
            {
                _scoreController.AddScore(false, birdController.isMoving);
            }
        }
        
        float tempScore = _scoreController.GetPhotoScore(birdController.spriteRenderer);
        
        latestPhoto.photoScore = tempScore;
        
        photoReady = false;
    }

    public void EndDay()
    {
        StartCoroutine(EndDayCoroutine());
    }
    
    public IEnumerator EndDayCoroutine()
    {
        yield return new WaitForSeconds(5f);
        
        Time.timeScale = 0;
        endDay.SetActive(true);
        print($"End of day, score was : {_scoreController.score}");

        switch (_scoreController.score)
        {
            case >= 0 and <= 49:
                starVersions[0].SetActive(true);
                break;
            
            case >= 50 and <= 99:
                starVersions[1].SetActive(true);
                break;
            
            case >= 100 and <= 149:
                starVersions[2].SetActive(true);
                break;
            case >= 150 and <= 199:
                starVersions[3].SetActive(true);
                break;
            
            case >= 200 and <= 249:
                starVersions[4].SetActive(true);
                break;
            
            case >= 250 and <= 300:
                starVersions[5].SetActive(true);
                break;
        }

        int index = 0;
        
        foreach (var kvp in birdPhotos)
        {
            foreach (var photo in kvp.Value)
            {
                if (index >= polaroidPositions.Count) break; //safety clause
                GameObject polaroid = Instantiate(polaroidPrefab, polaroidPositions[index]);
                var pUI = polaroid.gameObject.GetComponent<PolaroidUI>();
                pUI.polaroidImage.sprite = photo.PolaroidSprite;
                pUI.developer.color = new Color(0f, 0f, 0f, 0f);
                index++;
            }
        }

    }
    
    // private IEnumerator TargetsHintCoroutine()
    // {
    //     yield return new WaitForSeconds(2f);
    //     
    //     targetHint.SetActive(true);
    //     
    //     yield return new WaitForSeconds(10f);
    //     
    //     targetHint.SetActive(false);
    // }
}
