using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WinScreenManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameWordText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI longestWordText;
    [SerializeField] private TextMeshProUGUI wordsFoundText;
    [SerializeField] private TextMeshProUGUI definitionText;
    [SerializeField] private TextMeshProUGUI partOfSpeechText;
    [SerializeField] private TextMeshProUGUI exampleText;


    [SerializeField] private TemporaryDefinitionHolder TemporaryDefinitionHolder;

    private string longestWord;
    private int wordsFound;
    private List<string> wordsUsed = LevelData.levelData.wordsUsed;

    private IEnumerator Start()
    {
        yield return null;
        InitializeScreenData();
    }

    public void SetScreenData(string gameWord, int finalScore, int highScore, List<string> wordsUsed, int levelID)
    {
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
        gameWordText.text += LevelData.levelData.gameWord;
        currentScoreText.text = LevelData.levelData.totalPoints.ToString();
        longestWordText.text += longestWord;
        wordsFoundText.text += wordsFound.ToString();
        exampleText.text = LevelData.levelData.gameWord;

        definitionText.text = TemporaryDefinitionHolder.TemporaryDefinitions[LevelData.levelData.levelID - 1][0];
        partOfSpeechText.text = TemporaryDefinitionHolder.TemporaryDefinitions[LevelData.levelData.levelID - 1][1];
    }
}
