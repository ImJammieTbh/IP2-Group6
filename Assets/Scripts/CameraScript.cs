using UnityEngine;

public class CameraLag : MonoBehaviour
{
    public Transform Cam;          // Player Camera
    public float rotationLag = 10f;   // Higher = snappier, lower = heavier, just for if we use different cameras/lenses, the weight/lag can easily be edited

    void LateUpdate()
    {
       
        transform.rotation = Quaternion.Slerp // smoothly rotate toward the target rotation
        (
            transform.rotation,
            Cam.rotation,
            rotationLag * Time.deltaTime
        );
    }
}

