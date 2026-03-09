using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameMenuController : MonoBehaviour
{
    [Header("Text")]
    public TMP_Text pauseText;

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
    bool isPaused = false;
    public GameObject cameraObject; // disables the camera so it doesn't block the menu

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //text
        pauseText.gameObject.SetActive(false);

        //Volune
        volume.gameObject.SetActive(false);

        //Settings
        settings.gameObject.SetActive(false);

        //Buttons
        pauseButtons.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && isPaused == false)  //|| add controller button 
        {
            // pausing
            isPaused = true;
            Time.timeScale = 0; // stops gameplay  ,  need a way to stop camera from beng effected
            Cursor.lockState = CursorLockMode.None; // unlocks the cursor
            cameraObject.gameObject.SetActive(false);

            //add time pause, controller buttons, also add way for controller to hover, unlock cusor, link fov and sensitivity

            pauseText.gameObject.SetActive(true); // pause text
            pauseButtons.gameObject.SetActive(true); // pause menu buttons
        }
        else if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && isPaused == true) //unpausing
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
