using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public List<BirdData> targetBirds;

    public float score;

    public void CheckValidBird(BirdData shotBird)
    {
        if (targetBirds.Count == 0)
        {
            print("Target birds are empty");
        }

        foreach (BirdData targetBird in targetBirds)
        {
            if (shotBird == targetBird)
            {
                print("correct bird shot");
                score += 10;
                print("Score: " + score);
            }
        }
    }
}
