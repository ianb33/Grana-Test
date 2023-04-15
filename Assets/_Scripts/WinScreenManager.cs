using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WinScreenManager : MonoBehaviour
{
    [SerializeField] private int levelID;
    [SerializeField] private TextMeshProUGUI gameWordText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI longestWordText;
    [SerializeField] private TextMeshProUGUI wordsFoundText;
    [SerializeField] private TextMeshProUGUI definitionText;
    [SerializeField] private TextMeshProUGUI partOfSpeechText;
    [SerializeField] private TextMeshProUGUI exampleText;


    [SerializeField] private TemporaryDefinitionHolder TemporaryDefinitionHolder;
    [SerializeField] private PauseScreenManager pauseScreenManager;

    private string gameWord;
    private int finalScore;
    private int highScore;
    private string longestWord;
    private int wordsFound;

    private string[] wordsUsed;

    private IEnumerator Start()
    {
        SendScoreData();
        yield return null;
        InitializeScreenData();
    }

    public void SetScreenData(string gameWord, int finalScore, int highScore, List<string> wordsUsed, int levelID)
    {
        this.gameWord = gameWord;
        this.finalScore = finalScore;
        this.highScore = highScore;
        this.levelID = levelID;

        pauseScreenManager.levelID = levelID + 1;
        pauseScreenManager.gameWord = gameWord;

        wordsUsed.Remove(gameWord);

        wordsFound = wordsUsed.Count;

        wordsUsed.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);

        if (wordsUsed.Count > 0)
        {
            longestWord = wordsUsed[0];
        }
    }

    public void InitializeScreenData()
    {
        gameWordText.text += gameWord;
        currentScoreText.text = finalScore.ToString();
        longestWordText.text += longestWord;
        wordsFoundText.text += wordsFound.ToString();
        exampleText.text = gameWord;

        definitionText.text = TemporaryDefinitionHolder.TemporaryDefinitions[levelID - 1][0];
        partOfSpeechText.text = TemporaryDefinitionHolder.TemporaryDefinitions[levelID - 1][1];
    }

    private async void SendScoreData()
    {
        Debug.Log("Sending score data...");
        BackendManager backendManager = this.AddComponent<BackendManager>();

        //Set up WWWForm with required fields
        WWWForm SendScoreRequest = new WWWForm();
        SendScoreRequest.AddField("levelID", levelID);
        SendScoreRequest.AddField("score", finalScore);

        string requestResult = await backendManager.POSTRequest("https://grana.vinniehat.com/api/score/submit", SendScoreRequest);
    }
}
