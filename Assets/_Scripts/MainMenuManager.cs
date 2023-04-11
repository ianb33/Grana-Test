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

    [SerializeField] private TransitionManager transitionManager;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(() => PlayButtonClicked());
        tutorialButton.onClick.AddListener(() => TutorialButtonClicked());
    }

    public void PlayButtonClicked()
    {
        StartCoroutine(transitionManager.StartSceneTransition("LevelsScreen"));
    }

    public void TutorialButtonClicked()
    {
        StartCoroutine(transitionManager.StartSceneTransition("TutorialScreen"));
    }

    public void BackButtonClicked()
    {
        StartCoroutine(transitionManager.StartSceneTransition("MainMenu"));
    }
}
