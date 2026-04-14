using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdBrain : MonoBehaviour
{
    public LandingSpot currentSpot;

    [Range(0f, 1f)] public float chanceToLand;

    public bool WillLand()
    {
        if (Random.value < chanceToLand)
        {
            return false;
        }
        return true;
    }
    
    public LandingSpot ChooseNextLandingSpot(List<LandingSpotGroup> groups, string ID, List<LandingSpotGroup.LandingTypes> allowedLandingTypes)
    {
        if (currentSpot != null)
        {
            currentSpot.Release();
            currentSpot = null;
        }
        
        // Get all available spots across all non-full groups
        var allAvailableSpots = groups
            .Where(g => !g.isFull && allowedLandingTypes.Contains(g.landingType))
            .SelectMany(g => g.GetAvailableLandingSpots())
            .Where(spot => !spot.isOccupied)
            .ToList();

        if (allAvailableSpots.Count == 0)
        {
            Debug.Log("No available spots Anywhere");
            return null;
        }
        
        currentSpot = allAvailableSpots[Random.Range(0, allAvailableSpots.Count)];
        currentSpot.Occupy();
        
        Debug.Log($"Bird: {ID} Selected spot = [{currentSpot.name}]");
        return currentSpot;
    }
}
