using UnityEngine;

public class BestiaryAnimation : MonoBehaviour
{
    public float moveAmount = 100f; // how far up it moves
    public float moveSpeed = 5f; // how fast it moves
    public KeyCode toggleKey = KeyCode.Space;

    private RectTransform rectTransform;
    private Vector2 startPos;
    private Vector2 targetPos;
    private bool isUp = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
        targetPos = startPos;
    }

    // Update is called once per frame
    void Update()
    {
      //Toggle position when key is pressed
      if (Input.GetKeyDown(toggleKey))
        {
            isUp = !isUp;

            if (isUp)
                targetPos = startPos + new Vector2(0, moveAmount);
            else
                targetPos = startPos;
        }

        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition,targetPos,Time.deltaTime * moveSpeed);

        //snap to exact position when close 
        if (Vector2.Distance(rectTransform.anchoredPosition, targetPos) < 0.1f)
        {
            rectTransform.anchoredPosition = targetPos;
        }
    }
}
