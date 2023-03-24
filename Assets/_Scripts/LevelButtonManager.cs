using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private int ID;
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
        ID = (Array.IndexOf(buttons, button) + 1);

        StartCoroutine(LoadSceneAsync("GameScreen"));
        Debug.Log(ID);
    }

    private IEnumerator LoadSceneAsync(string SceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);

        asyncOperation.completed += operation =>
        {
            // Find the "Managers" GameObject in the scene hierarchy
            GameObject managerObject = GameObject.Find("GameManager");

            if (managerObject == null)
            {
                Debug.LogError("Unable to find the 'Managers' GameObject in the scene hierarchy.");
                return;
            }

            // Get the GameManager component from the "Managers" GameObject
            GameManager gameManager = managerObject.GetComponent<GameManager>();

            if (gameManager == null)
            {
                Debug.LogError("Unable to find the 'GameManager' component on the 'Managers' GameObject.");
                return;
            }

            // Call a method on the GameManager to pass the level ID
            gameManager.SetLevelID(ID);
        };

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
