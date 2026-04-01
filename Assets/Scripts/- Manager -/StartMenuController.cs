using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartMenuController : MonoBehaviour
{
    // Tutorial referenced: https://youtu.be/paaBTt5GcMU?si=q2NMNfsy82rxqIzv
    // Tutorial referenced: https://youtu.be/Hn804Wgr3KE?si=_mwzCvpTF7qrhE33

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
    public Slider masterSlider; //assume this will allow the slider's value to carry over between scenes
    public Slider sfxSlider;
    public Slider birdsSlider;
    public Slider musicSlider;
    public Slider backgroundSlider;


    [Header("Settings")]
    public GameObject settings; // All the misc settings UI elements
    public Slider fovSlider;
    public Slider sensitivitySlider;


    //CONTROLS if add key bindings or controller manual in start manual
    //public GameObject controls;

    [Header("Buttons")]
    public Button startButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backButton;

    // Static values to carry across scenes
    //Volume
    public static float masterVol = 1;
    public static float sfxVol = 1;
    public static float birdsVol = 1;
    public static float musicVol = 1;
    public static float backgroundVol = 1;

    //Setting
    public static float sensitivityVal = 100;
    public static float fovVal = 1;



    // Sets all the setting UI elements as inactive (hidden)
    void Start()
    {
        //Volume
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
        masterValue.SetText((Mathf.Round(masterSlider.value * 100)).ToString());
        sfxValue.SetText((Mathf.Round(sfxSlider.value * 100)).ToString());
        birdsValue.SetText((Mathf.Round(birdsSlider.value * 100)).ToString());
        musicValue.SetText((Mathf.Round(musicSlider.value * 100)).ToString());
        backgroundValue.SetText((Mathf.Round(backgroundSlider.value * 100)).ToString());

        //Settings
        fovValue.SetText(fovSlider.value.ToString());
        sensitivityValue.SetText(sensitivitySlider.value.ToString());



        // making values static to carry over scenes

        //Volume
        masterVol = masterSlider.value;
        sfxVol = sfxSlider.value;
        birdsVol = birdsSlider.value;
        musicVol = musicSlider.value;
        backgroundVol = backgroundSlider.value;

        //Setting
        sensitivityVal = sensitivitySlider.value;
        fovVal = fovSlider.value;
    }


    //START GAME
    public void OnStartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Moves to the scene named in the brackets
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


        backButton.Select(); // selects the back button when pressing the settings button
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


        startButton.Select(); // selects the start button when pressing the back button
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
