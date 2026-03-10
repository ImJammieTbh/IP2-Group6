using UnityEngine;
using TMPro;

public class BestiaryObjectiives : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text ObjectiveDisplay1; //reference to the ui text
    private int Objvalue = 0; // setting objective to 0 
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Q))
        {
            Objvalue = 1;
            UpdateText();
        }    
    }

    void UpdateText()
    {
        ObjectiveDisplay1.text = Objvalue.ToString();
    }
}
