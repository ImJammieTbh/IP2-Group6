using System.Collections;
using UnityEngine;

public class LeafShutterPart : MonoBehaviour
{
    public float rotateTime = 0.1f;
    public float holdTime = 0.1f;

    public RectTransform Blade;
    public RectTransform Open;
    public RectTransform Closed;

    public void Fire()
    {
        StartCoroutine(RotateRoutine());
    }

    IEnumerator RotateRoutine()
    {
        yield return RotateBetween(Open.localEulerAngles, Closed.localEulerAngles, rotateTime); // calls rotation for closing
        yield return new WaitForSeconds(holdTime);                                              // waits a bit
        yield return RotateBetween(Closed.localEulerAngles, Open.localEulerAngles, rotateTime); // calls rotation for opening
    }

    IEnumerator RotateBetween(Vector3 from, Vector3 to, float time)
    {
        float t = 0f;

        while (t < time)
        {
            t += Time.deltaTime;
            float lerp = t / time;

            Vector3 angle = Vector3.Lerp(from, to, lerp);

            Blade.localEulerAngles = new Vector3(0f, 0f, angle.z); // found a new way to work with this stuff, so I might change up the polaroid ui to make it cleaner n like this

            yield return null;
        }
    }
}


