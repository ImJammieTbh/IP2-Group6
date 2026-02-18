using UnityEngine;

public class LandingSpot : MonoBehaviour
{
    public bool isOccupied;

    public void Occupy()
    {
        isOccupied = true;
    }
    
    public void Release()
    {
        isOccupied = false;
    }
}
