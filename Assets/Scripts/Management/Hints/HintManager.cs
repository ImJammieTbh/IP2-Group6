using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public enum HintType {Controller, Mouse}


public class HintManager : MonoBehaviour
{
    [Header("Controller")] 
    public GameObject controllerParent;
    public GameObject viewHintC;
    public GameObject lookHintC;
    public GameObject zoomHintC;
    public GameObject captureHintC;
    
    [Header("Mouse")]
    public GameObject mouseParent;
    public GameObject viewHintM;
    public GameObject lookHintM;
    public GameObject zoomHintM;
    public GameObject captureHintM;

    private GameObject lookHint;
    private GameObject captureHint;
    private GameObject viewHint;
    private GameObject zoomHint;

    public float idleTimerMax;

    private GameObject _player;
    private float _idleTimer;

    private Quaternion _lastPlayerRotation;
    private CameraLag _cameraLag;
    
    public HintType hintType;
    
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _lastPlayerRotation = _player.transform.rotation;
        
        _cameraLag = GameObject.Find("CamHolder").GetComponent<CameraLag>();
    }

    private void Update()
    {
        if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            hintType = HintType.Controller;
            Debug.Log("Using Controller");
        }
        else if (Mouse.current.delta.ReadValue() != Vector2.zero)
        {
            hintType = HintType.Mouse;
            Debug.Log("Using Mouse");
        }

        switch (hintType)
        {
            case HintType.Controller:
                controllerParent.SetActive(true);
                mouseParent.SetActive(false);
                
                lookHint = lookHintC;
                viewHint = viewHintC;
                zoomHint = zoomHintC;
                captureHint = captureHintC;
                break;
            
            case HintType.Mouse:
                controllerParent.SetActive(false);
                mouseParent.SetActive(true);
                
                lookHint = lookHintM;
                viewHint = viewHintM;
                zoomHint = zoomHintM;
                captureHint = captureHintM;
                break;
        }
        
        lookHint.SetActive(HasRotationBeenIdle());

        if (!_cameraLag.isAiming)
        {
            viewHint.SetActive(true);
            captureHint.SetActive(false);
            zoomHint.SetActive(false);
        }
        else 
        {
            viewHint.SetActive(false);
            captureHint.SetActive(true);
            zoomHint.SetActive(true);
        }
    }

    private bool HasRotationBeenIdle()
    {
        float angleDifference = Quaternion.Angle(_player.transform.rotation, _lastPlayerRotation);

        if (angleDifference > 2f)
        {
            _idleTimer = 0f;
            _lastPlayerRotation = _player.transform.rotation;
            return false;
        }

        _idleTimer += Time.deltaTime;
        return _idleTimer >= idleTimerMax;
    }
}
