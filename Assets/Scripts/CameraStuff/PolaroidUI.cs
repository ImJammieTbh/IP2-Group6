using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PolaroidUI : MonoBehaviour
{
    public Image polaroidImage;
    public Image developer;
    public RectTransform rect;
    public AudioSource eject;
    public float slideTime = 3f; // also develop time
    public float devTime = 1f;
    public float holdTime = 1.5f;

    private Vector2 offscreenPos;
    private Vector2 onscreenPos;
    private PolaroidEjector ejector;

    public void Init(Sprite sprite, PolaroidEjector ejectorRef)
    {
        ejector = ejectorRef;
        polaroidImage.sprite = sprite;

        rect = GetComponent<RectTransform>();

        onscreenPos = rect.anchoredPosition;

        offscreenPos = new Vector2(onscreenPos.x, onscreenPos.y - 100f);

        rect.anchoredPosition = offscreenPos;

        StartCoroutine(SlideRoutine());
        StartCoroutine(DevelopRoutine());
    }

    IEnumerator DevelopRoutine()
    {
        float t = 0f;
        Color c = developer.color;

        while (t < devTime)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / devTime);
            developer.color = new Color(c.r, c.g, c.b, alpha);
            yield return null;
        }

        developer.color = new Color(c.r, c.g, c.b, 0f);
    }


    IEnumerator SlideRoutine()
    {
        eject.volume = 0.25f;
        eject.pitch = Random.Range(0.9f, 1.1f);

        eject.Play();

        float t = 0f;
        while (t < slideTime/3)
        {
            t += Time.deltaTime;
            rect.anchoredPosition = Vector2.Lerp(offscreenPos, onscreenPos/3, t / slideTime); // slide in 1
            yield return null;
        }

        eject.Pause();
        yield return new WaitForSeconds(holdTime / 3);
        eject.Play();

        while (t < (slideTime/3)*2 && t >slideTime/3)
        {
            t += Time.deltaTime;
            rect.anchoredPosition = Vector2.Lerp(offscreenPos, (onscreenPos/3)*2, t / slideTime); // slide in 2
            yield return null;
        }

        eject.Pause();
        yield return new WaitForSeconds(holdTime / 3);
        eject.Play();

        while (t < slideTime && t > (slideTime / 3)*2)
        {
            t += Time.deltaTime;
            rect.anchoredPosition = Vector2.Lerp(offscreenPos, onscreenPos, t / slideTime); // slide in final
            yield return null;
        }

        eject.Stop();

        rect.anchoredPosition = onscreenPos;


        yield return new WaitForSeconds(holdTime); // stops on screen

        t = 0f;
        while (t < slideTime)
        {
            t += Time.deltaTime;
            rect.anchoredPosition = Vector2.Lerp(onscreenPos, offscreenPos, t / slideTime); // slides out
            yield return null;
        }

        ejector.isBusy = false;
        
        Destroy(gameObject);
    }
}


