using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//This script can be used as a template for any UI menu type; for the tutorial I made this script for, I discuss a how to make a Pause Screen, but you can use this exact logic to control any menu type and switch between the different screens on that menu.
public class PauseScreenManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Button resumeGameButton;
    [SerializeField] Button pauseGameButton;
    [SerializeField] Button backToMainMenuButton;

    private bool paused = false;

    private void Start()
    {
        pauseScreen.SetActive(false);
        if (pauseGameButton != null)
        {
            pauseGameButton.onClick.AddListener(() => PauseGameButtonClicked());
        }
        if (resumeGameButton != null)
        {
            resumeGameButton.onClick.AddListener(() => ResumeButtonClicked());
        }
        if (backToMainMenuButton != null)
        {
            backToMainMenuButton.onClick.AddListener(() => BackToMainMenuButtonClicked());
        }
    }

    public void PauseGameButtonClicked()
    {
        pauseScreen.SetActive(true);
        Debug.Log("Paused");
    }

    public void ResumeButtonClicked()
    {
        pauseScreen.SetActive(false);
    }

    public void BackToMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

}