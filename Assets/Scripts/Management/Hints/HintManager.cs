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
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _lastPlayerRotation = _player.transform.rotation;
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
    }

    private bool HasRotationBeenIdle()
    {
        float angleDifference = Quaternion.Angle(transform.rotation, _lastPlayerRotation);

        if (angleDifference > 2f)
        {
            _idleTimer = 0f;
            _lastPlayerRotation = transform.rotation;
            return false;
        }

        _idleTimer += Time.deltaTime;
        return _idleTimer >= idleTimerMax;
    }
}
