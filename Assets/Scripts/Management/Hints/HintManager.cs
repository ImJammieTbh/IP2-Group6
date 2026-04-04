using System;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public GameObject viewHint;
    public GameObject lookHint;
    public GameObject zoomHint;
    public GameObject captureHint;

    public float idleTimerMax;

    private GameObject _player;
    private float _idleTimer;

    private Quaternion _lastPlayerRotation;
    private CameraLag _cameraLag;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _lastPlayerRotation = _player.transform.rotation;
        
        _cameraLag = GameObject.Find("CamHolder").GetComponent<CameraLag>();
    }

    private void Update()
    {
        if (HasRotationBeenIdle())
        {
            lookHint.SetActive(true);
        }
        else
        {
            lookHint.SetActive(false);
        }

        if (!_cameraLag.isAiming)
        {
            viewHint.SetActive(true);
        }
        else 
        {
            viewHint.SetActive(false);
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
