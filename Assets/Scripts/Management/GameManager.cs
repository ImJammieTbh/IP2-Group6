using System;
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
    
    public List<PhotoData> photos;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _scoreController = GetComponent<ScoreController>();
        
        CameraLag.OnPhotoTaken += CheckValidBird;
        PolaroidEjector.OnEjected += GetRecentPhoto;
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

         
    }
    
    public void UpdateScore(BirdController birdController)
    {
        _scoreController.score = _scoreController.GetPhotoScore(birdController.spriteRenderer);
        scoreText.text = "Score: " + _scoreController.score;
    }

    public void GetRecentPhoto()
    {
        photos.Add(photoManager.LatestPhoto);
    }
}
