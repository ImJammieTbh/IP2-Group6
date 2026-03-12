using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ScoreController _scoreController;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _scoreController = GetComponent<ScoreController>();
        
        CameraLag.OnPhotoTaken += UpdateScore;
    }

    public void UpdateScore(BirdData data)
    {
        _scoreController.CheckValidBird(data);
    }
}
