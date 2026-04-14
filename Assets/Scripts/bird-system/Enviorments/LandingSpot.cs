using System;
using UnityEngine;

public class LandingSpot : MonoBehaviour
{
    public bool isOccupied;
    
    [Header("Gizmo Configuration")]
    public Color gizmoColor;
    public float gizmoRadius;

    public void Occupy()
    {
        isOccupied = true;
    }
    
    public void Release()
    {
        isOccupied = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius);
    }
}
