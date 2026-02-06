using System;
using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private string _birdName;
    private float _birdSpeed;

    private void Start()
    {
        FlyTo();
    }

    private void FlyTo()
    {
        
    }

    private IEnumerable FlyToCoroutine()
    {
        
        
        yield return null;
    }
}
