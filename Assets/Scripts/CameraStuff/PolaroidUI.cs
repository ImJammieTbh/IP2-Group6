using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PolaroidUI : MonoBehaviour
{
    public Image polaroidImage;
    public RectTransform rect;
    public float slideTime = 0.3f;
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
    }

    IEnumerator SlideRoutine()
    {
        
        float t = 0f;
        while (t < slideTime)
        {
            t += Time.deltaTime;
            rect.anchoredPosition = Vector2.Lerp(offscreenPos, onscreenPos, t / slideTime); // slide in
            yield return null;
        }

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


