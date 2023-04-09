using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] public float gameTime;
    [SerializeField] public GameObject timer;
    [SerializeField] public float duration;
    public float deltaTime = 0.0f;
    private Color endColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        timer.GetComponent<Image>().color = Color.green;
        duration = gameTime;
        StartCoroutine(UpdateColorCR());
        sendScoreData();
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
        GameObject BackgroundBlur = Instantiate((GameObject)Resources.Load("Prefabs/BackgroundBlur"), GameObject.Find("GUICanvas").GetComponent<Transform>());
        BackgroundBlur.transform.right = Vector3.zero;
        BackgroundBlur.transform.up = Vector3.zero;
        BackgroundBlur.transform.SetSiblingIndex(3);

        //show alert
        GameObject.Find("GameManager").GetComponent<GameManager>().DisplayAlert("endAlert", "Time's up!", 0.4f, 1f, 200
            , 2);
    }

    private async void sendScoreData()
    {
        BackendManager backendManager = this.AddComponent<BackendManager>();
        string PlayerID = PlayerPrefs.GetString("PlayerID");
        backendManager.POSTRequest("https://grana.vinniehat.com/api/auth/signup", PlayerID);
    }


}
