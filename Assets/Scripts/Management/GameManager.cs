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

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _scoreController = GetComponent<ScoreController>();
        
        CameraLag.OnPhotoTaken += CheckValidBird;
        PolaroidEjector.OnEjected += PhotoEjectedCall;
    }

    private void FixedUpdate()
    {
        if (!birdsCaptured && capturedBirds.Count == targetBirds.Count)
        {
            birdsCaptured = true;
            print("Birds captured --- Level Complete");
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
}
