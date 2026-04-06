using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameMenuController : MonoBehaviour
{
    [Header("UI")]
    public GameObject crosshair;

    [Header("Text")]
    public TMP_Text pauseText;

    [Header("background")]
    public GameObject menuBackground;
    public GameObject settingsBackground;


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
    public GameObject pauseButtons;
    public Button resumeButton;
    public Button settingsButton;
    public Button mainMenuButton;
    public Button quitButton;
    public Button backButton;

    [Header("Pausing")]
    public bool isPaused = false;
    public GameObject cameraObject; // disables the camera so it doesn't block the menu
    //stop all audio when paused, source: https://discussions.unity.com/t/how-to-stop-all-audio/32919


    [Header("SFX")] //for clicking the buttons
    private AudioSource[] allAudioSources;
    public BirdEmitter birdSounds;
    public AudioSource radioMusic; //starts playing once unpaused
    public float buttonVolume = 0.5f;
    public AudioSource buttonSoundClips;
    


    // Setting all assets as inactive
    void Start()
    {
        //Background
        menuBackground.gameObject.SetActive(false);
        settingsBackground.gameObject.SetActive(false);

        //Text
        pauseText.gameObject.SetActive(false);

        //Volune
        volume.gameObject.SetActive(false);

        //Settings
        settings.gameObject.SetActive(false);

        //Buttons
        pauseButtons.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);


        // Carry over the value of the sliders over to the current scene
        //Volume
        masterSlider.value = StartMenuController.masterVol;
        sfxSlider.value = StartMenuController.sfxVol;
        birdsSlider.value = StartMenuController.birdsVol;
        musicSlider.value = StartMenuController.musicVol;
        backgroundSlider.value = StartMenuController.backgroundVol;

        //Settings
        sensitivitySlider.value = StartMenuController.sensitivityVal;
        fovSlider.value = StartMenuController.fovVal;
    }


    // Seeing when game is paused and update volume
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && isPaused == false)  // pausing
        {
            //AudioListener.volume = 0; // TEMP SOLVER, mute all sounds, because if pause when a sound effect is playing it will play the whole sound
            //StopAllAudio(); 

            resumeButton.Select();

            // pausing
            isPaused = true;
            Time.timeScale = 0; // stops gameplay  ,  need a way to stop camera from beng effected
            Cursor.lockState = CursorLockMode.None; // unlocks the cursor
            cameraObject.gameObject.SetActive(false);

            //add time pause, controller buttons, also add way for controller to hover, unlock cusor, link fov and sensitivity

            pauseText.gameObject.SetActive(true); // pause text
            pauseButtons.gameObject.SetActive(true); // pause menu buttons
            menuBackground.gameObject.SetActive(true);

            //Audio
            birdSounds.quackCoolDown = 0;
            radioMusic.volume = 0.05f; // lower the songs volume
            //radioMusic.Play();

            //UI
            crosshair.gameObject.SetActive(false);
        }
        else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && isPaused == true) //unpausing
        {
            //AudioListener.volume = 1;

            // unpausing
            isPaused = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked; // locks cursor
            cameraObject.gameObject.SetActive(true);

            //text
            pauseText.gameObject.SetActive(false);

            //Volune
            volume.gameObject.SetActive(false);

            //Settings
            settings.gameObject.SetActive(false);

            //Buttons
            pauseButtons.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);

            //Background
            menuBackground.gameObject.SetActive(false);
            settingsBackground.gameObject.SetActive(false);

            //Audio
            birdSounds.quackCoolDown = 0;
            radioMusic.volume = 0.25f;

            //UI
            crosshair.gameObject.SetActive(true);

        }

        //Volume 
        masterValue.SetText((Mathf.Round(masterSlider.value * 100)).ToString());
        sfxValue.SetText((Mathf.Round(sfxSlider.value * 100)).ToString());
        birdsValue.SetText((Mathf.Round(birdsSlider.value * 100)).ToString());
        musicValue.SetText((Mathf.Round(musicSlider.value * 100)).ToString());
        backgroundValue.SetText((Mathf.Round(backgroundSlider.value * 100)).ToString());

        //Settings
        fovValue.SetText(fovSlider.value.ToString());
        sensitivityValue.SetText(sensitivitySlider.value.ToString());
    }

    void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }








    //RESUME
    public void OnResumeClick()
    {
        // unpausing
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked; // locks cursor
        cameraObject.gameObject.SetActive(true);

        //text
        pauseText.gameObject.SetActive(false);

        //Volune
        volume.gameObject.SetActive(false);

        //Settings
        settings.gameObject.SetActive(false);

        //Buttons
        pauseButtons.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);

        //Background
        menuBackground.gameObject.SetActive(false);
    }

    //SETTINGS
    public void OnSettingsClick()
    {
        //Text
        pauseText.gameObject.SetActive(false);

        //Volune
        volume.gameObject.SetActive(true);

        //Settings
        settings.gameObject.SetActive(true);

        //Buttons
        backButton.gameObject.SetActive(true);

        pauseButtons.gameObject.SetActive(false);

        //Background
        menuBackground.gameObject.SetActive(false);
        settingsBackground.gameObject.SetActive(true);

        backButton.Select(); // selects the back button when pressing the settings button
    }


    //RETURN TO MAIN MENU
    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu"); // Moves to the scene named in the brackets
    }


    //BACK
    public void OnBackClick()
    {
        //Text
        pauseText.gameObject.SetActive(true);

        //Volune
        volume.gameObject.SetActive(false);

        //Settings
        settings.gameObject.SetActive(false);

        //Buttons
        backButton.gameObject.SetActive(false);

        pauseButtons.gameObject.SetActive(true);

        //Background
        menuBackground.gameObject.SetActive(true);
        settingsBackground.gameObject.SetActive(false);


        resumeButton.Select(); // selects the resume button when pressing the back button
    }


    //CLICK SOUND
    public void OnClickSound()
    {
        buttonSoundClips.volume = buttonVolume;
        buttonSoundClips.Play();
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
