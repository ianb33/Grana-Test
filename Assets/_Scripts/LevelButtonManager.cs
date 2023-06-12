using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private TransitionManager transitionManager;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => buttonPressedCallback(button));
        }
    }

    public void buttonPressedCallback(Button button)
    {
        LevelData.levelData.levelID = Array.IndexOf(buttons, button) + 1;

        StartCoroutine(transitionManager.StartSceneTransition("GameScreen"));
        Debug.Log(LevelData.levelData.levelID);
    }
}
