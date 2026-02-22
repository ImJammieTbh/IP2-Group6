using System.Collections;
using UnityEngine;

public class PolaroidBehaviour : MonoBehaviour
{
    public Transform followTarget;   // set by the ejector
    public float followLag = 10f;    // higher = snappier, lower = floaty
    public float ejectDistance = 0.3f;
    public float ejectTime = 0.2f;
    public float holdTime = 1.0f;
    public float fallDistance = 1.0f;
    public float fallTime = 0.6f;

    private Vector3 localStartPos;

    void Start()
    {
        localStartPos = transform.localPosition;
        StartCoroutine(Popout());
    }

    void Update()
    {
        if (followTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, Time.deltaTime * followLag); // smoothly follows the camera holder, similarly to how the camera holder follows the main cam
        }
    }

    IEnumerator Popout()
    {
        Vector3 ejectPos = localStartPos + Vector3.forward * ejectDistance; // pops out
        yield return MoveLocal(localStartPos, ejectPos, ejectTime); 

        yield return new WaitForSeconds(holdTime); // waits

        Vector3 fallPos = ejectPos + Vector3.down * fallDistance; // falls down
        yield return MoveLocal(ejectPos, fallPos, fallTime);

        Destroy(gameObject); // DIES
    }

    IEnumerator MoveLocal(Vector3 from, Vector3 to, float time)
    {
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(from, to, t / time);
            yield return null;
        }
        transform.localPosition = to;
    }
}

