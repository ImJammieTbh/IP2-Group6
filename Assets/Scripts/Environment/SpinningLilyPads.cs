using UnityEngine;

public class SpinningLilyPads : MonoBehaviour
{
    Vector3 rotate;
    public float spinspeed = 1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, spinspeed);
    }
}
