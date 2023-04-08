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
    [SerializeField] private Button resumeGameButton;
    [SerializeField] private Button pauseGameButton;
    [SerializeField] private Button backToMainMenuButton;
    [SerializeField] private Button backToLevelsButton;
    [SerializeField] private GameTimer gameTimer;

    private bool paused = false;

    private void Start()
    {
        if (backToLevelsButton != null)
        {
            backToLevelsButton.onClick.AddListener(() => BackToLevelsClicked());
        }
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
        if (gameTimer != null)
        {
            gameTimer.TogglePause();
        }
        pauseScreen.SetActive(true);
        Debug.Log("Paused");
    }

    public void ResumeButtonClicked()
    {
        if (gameTimer != null)
        {
            gameTimer.TogglePause();
        }
        pauseScreen.SetActive(false);
    }

    public void BackToMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToLevelsClicked()
    {
        SceneManager.LoadScene("LevelsScreen");
    }
}