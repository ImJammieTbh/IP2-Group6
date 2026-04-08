using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public InputAction interact;

    [Header("MenuObjects")] 
    public GameObject[] pages;

    private int _CurrentPageIndex = 0;

    private IEnumerator EnableInteract()
    {
        yield return new WaitForEndOfFrame();
        
        interact.Enable();
    }

    private void Start()
    {
        StartCoroutine(EnableInteract());
    }

    private void Update()
    {
        if (interact.triggered || Input.GetKeyDown(KeyCode.Space))
        {
            NextSection();
        }
    }

    private void NextSection()
    {
        if (_CurrentPageIndex < pages.Length)
        {
            // Turn off current
            pages[_CurrentPageIndex].SetActive(false);

            // Move to next index
            _CurrentPageIndex++;

            // Loop back if at end
            if (_CurrentPageIndex >= pages.Length)
            {
                LoadNextScene();
            }

            // Turn on next
            pages[_CurrentPageIndex].SetActive(true);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
