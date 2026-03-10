using UnityEngine;

public class Bestiary : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject BestiaryUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    // Hiding the object by changing its alpha so the GameObject can still run and do what it needs to
    // future changes: putting it to the left hand side of the screen and have it choose and display random numbers
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) //setting the controls to B
        {
            CanvasGroup group = BestiaryUI.GetComponent<CanvasGroup>(); // checks if the canvas group component is attached and stores BestiaryUI into a variable called "group"

            if (group == null) //checks if BestiaryUI isnt in a group 
                group = BestiaryUI.AddComponent<CanvasGroup>(); //if it doesnt have the canvas group component it adds it 

            //hides the square without removing the object from the game
            group.alpha = group.alpha == 0 ? 1 : 0;
            group.blocksRaycasts = group.alpha == 1;

        }
    }
}
