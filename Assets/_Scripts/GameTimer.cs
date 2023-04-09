using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] public float gameTime;
    [SerializeField] public GameObject timer;
    [SerializeField] public float duration;
    [SerializeField] private GameManager gameManager;

    public float deltaTime = 0.0f;
    private Color endColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        timer.GetComponent<Image>().color = Color.green;
        duration = gameTime;
        StartCoroutine(UpdateColorCR());
    }

    public IEnumerator UpdateColorCR()
    {
        Color startColor = timer.GetComponent<Image>().color;
        float duration = gameTime;

        while (gameTime > 0)
        {
            gameTime -= deltaTime * Time.deltaTime;

            //update color
            timer.GetComponent<Image>().color = Color.Lerp(endColor, startColor, gameTime / duration);

            //update size
            float width = gameTime / this.duration * Screen.width;
            timer.GetComponent<RectTransform>().sizeDelta = new Vector2(width, Screen.height * 0.1f);

            yield return null;
        }

        if (gameTime <= 0)
        {
            Debug.Log("Time's up!");
            OnTimerEnd();
        }
    }

    public void TogglePause()
    {
        deltaTime = 1 - deltaTime;
    }

    private void OnTimerEnd()
    {
        //show alert
        gameManager.DisplayAlert("endAlert", "Time's up!", 0.4f, 1f, 100, 2);

        AsyncOperation sceneLoadAsync = SceneManager.LoadSceneAsync("WinScreen", LoadSceneMode.Single);


    }
}
