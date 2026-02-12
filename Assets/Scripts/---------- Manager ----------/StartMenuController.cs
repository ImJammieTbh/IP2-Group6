using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartMenuController : MonoBehaviour
{
    // Tutorial referenced: https://youtu.be/paaBTt5GcMU?si=q2NMNfsy82rxqIzv

    [Header("Text")]
    public TMP_Text gameTitle;

    // settings
    public TMP_Text fovValue;
    public TMP_Text sensitivityValue;

    // volume
    public TMP_Text masterValue;
    public TMP_Text sfxValue;
    public TMP_Text birdsValue;
    public TMP_Text musicValue;
    public TMP_Text backgroundValue;


    [Header("Volume")]
    public GameObject volume; // All the volume UI elements
    public static Slider masterSlider; //assume this will allow the slider's value to carry over between scenes
    public static Slider sfxSlider;
    public static Slider birdsSlider;
    public static Slider musicSlider;
    public static Slider backgroundSlider;


    [Header("Settings")]
    public GameObject settings; // All the misc settings UI elements
    public static Slider fovSlider;
    public static Slider sensitivitySlider;


    //CONTROLS if add key bindings or controller manual in start manual
    //public GameObject controls;

    [Header("Buttons")]
    public Button startButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backButton;



    // Sets all the setting UI elements as inactive (hidden)
    void Start()
    {
        //Volune
        volume.gameObject.SetActive(false);

        //Settings
        settings.gameObject.SetActive(false);

        //Buttons
        backButton.gameObject.SetActive(false);
    }

    // Update the text for the slider values
    void Update()
    {
        //Volume 
        masterValue.SetText((masterSlider.value * 100).ToString());
        sfxValue.SetText((sfxSlider.value * 100).ToString());
        birdsValue.SetText((birdsSlider.value * 100).ToString());
        musicValue.SetText((musicSlider.value * 100).ToString());
        backgroundValue.SetText((backgroundSlider.value * 100).ToString());

        //Settings
        fovValue.SetText(fovSlider.value.ToString());
        sensitivityValue.SetText(sensitivitySlider.value.ToString());
    }


    //START GAME
    public void OnStartClick()
    {
        SceneManager.LoadScene("SampleScene"); // Moves to the scene named in the brackets
    }


    //SETTINGS
    public void OnSettingsClick()
    {
        //Text
        gameTitle.gameObject.SetActive(false);

        //Volune
        volume.gameObject.SetActive(true);

        //Settings
        settings.gameObject.SetActive(true);

        //Buttons
        backButton.gameObject.SetActive(true);

        startButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }


    //BACK
    public void OnBackClick()
    {
        //Text
        gameTitle.gameObject.SetActive(true);

        //Volune
        volume.gameObject.SetActive(false);

        //Settings
        settings.gameObject.SetActive(false);

        //Buttons
        backButton.gameObject.SetActive(false);

        startButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }


    //QUITE GAME
    public void OnQuitClick()
    {

#if UNITY_EDITOR // For quiting when running in the unity editor
    UnityEditor.EditorApplication.isPlaying = false;
#endif

        // When the game is built
        Application.Quit();
    }
}
