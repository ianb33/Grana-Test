using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WinScreenManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameWordText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI longestWordText;
    [SerializeField] private TextMeshProUGUI wordsFoundText;

    private string gameWord;
    private int finalScore;
    private int highScore;
    private string longestWord;
    private int wordsFound;

    private string[] wordsUsed;



    public void SetScreenData(string gameWord, int finalScore, int highScore, List<string> wordsUsed)
    {
        gameWord = this.gameWord;
        finalScore = this.finalScore;
        highScore = this.highScore;

        wordsFound = wordsUsed.Count;

        wordsUsed.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);

        longestWord = wordsUsed[0];
    }

}
