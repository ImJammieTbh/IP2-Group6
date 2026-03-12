using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoreController _scoreController;
    
    public TMP_Text scoreText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _scoreController = GetComponent<ScoreController>();
        
        CameraLag.OnPhotoTaken += UpdateScore;
    }

    public void UpdateScore(BirdData data, BirdController birdController)
    {
        _scoreController.CheckValidBird(data, birdController);
        scoreText.text = "Score: " + _scoreController.score;
    }
}
