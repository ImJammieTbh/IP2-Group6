using UnityEngine;

public class PauseCancelSound : MonoBehaviour
{
    void Update()
    {
        if (Time.timeScale == 0)
        {
            Destroy(gameObject);
        }
    }
}
