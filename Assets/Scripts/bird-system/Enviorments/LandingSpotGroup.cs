using System;
using System.Collections.Generic;
using UnityEngine;

public class LandingSpotGroup : MonoBehaviour
{
    public bool isFull;
    
    public List<LandingSpot> landingSpots = new List<LandingSpot>();
    
    public List<LandingSpot> GetAvailableLandingSpots()
    {
        return landingSpots.FindAll(ls => !ls.isOccupied);
    }

    private void Update()
    {
        isFull = SpotsFull();
    }

    private bool SpotsFull()
    {
        return landingSpots.FindAll(ls => ls.isOccupied).Count == landingSpots.Count;
    }
}
