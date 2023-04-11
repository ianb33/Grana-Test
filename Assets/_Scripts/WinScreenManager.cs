using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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


    [SerializeField] private TemporaryDefinitionHolder TemporaryDefinitionHolder;

    private string gameWord;
    private int finalScore;
    private int highScore;
    private string longestWord;
    private int wordsFound;

    private string[] wordsUsed;

    private IEnumerator Start()
    {
        yield return null;
        InitializeScreenData();
    }

    public void SetScreenData(string gameWord, int finalScore, int highScore, List<string> wordsUsed, int levelID)
    {
        this.gameWord = gameWord;
        this.finalScore = finalScore;
        this.highScore = highScore;
        this.levelID = levelID;

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

        definitionText.text = TemporaryDefinitionHolder.TemporaryDefinitions[levelID - 1][0];
    }
}
