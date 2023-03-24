using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Button playButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(() => PlayButtonClicked());
        tutorialButton.onClick.AddListener(() => TutorialButtonClicked());
    }

    public void PlayButtonClicked()
    {
        SceneManager.LoadScene("LevelsScreen");
    }

    public void TutorialButtonClicked()
    {
        SceneManager.LoadScene("TutorialScreen");
    }

    public void BackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
