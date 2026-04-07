using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public float score;
    
    public Camera photoCamera;

    [Header("Score Weights")] 
    public float sizeWeight;
    public float centreWeight;
    
    public float GetPhotoScore(SpriteRenderer birdSpriteRenderer)
    {
        if (photoCamera == null || birdSpriteRenderer == null)
            return 0f;

        Bounds bounds = birdSpriteRenderer.bounds;

        //centre
        Vector3 viewPos = photoCamera.WorldToViewportPoint(bounds.center);

        if (viewPos.z < 0)
            return 0f;

        if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
            return 0f;

        Vector2 birdPos = new Vector2(viewPos.x, viewPos.y);
        Vector2 centre = new Vector2(0.5f, 0.5f);

        float centreDist = Vector2.Distance(birdPos, centre);
        float centreScore = Mathf.Clamp01(1f - (centreDist * 2f));
        
        //size
        Vector3 min = photoCamera.WorldToViewportPoint(bounds.min);
        Vector3 max = photoCamera.WorldToViewportPoint(bounds.max);

        float width = Mathf.Abs(max.x - min.x);
        float height = Mathf.Abs(max.y - min.y);

        float sizeScore = Mathf.Clamp01(width * height * 4f);
        
        float finalScore =
            (sizeScore * sizeWeight) +
            (centreScore * centreWeight);

        float clampedScore = Mathf.Clamp(finalScore, 0f, 100f);
        
        float scoreOutOf10 = clampedScore / 10f;
        
        return (float)Math.Round(scoreOutOf10, 1);
    }

    public void AddScore(bool isTarget, bool isMoving)
    {
        score += 10f;
        
        if (isTarget)
        {
            score += 30f;
        }

        if (!isMoving)
        {
            score += 8f;
        }
        
        print(score);
    }
}
