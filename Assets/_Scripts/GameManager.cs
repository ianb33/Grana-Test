using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [Header("Game Details")]
    [SerializeField] private int levelID;
    [SerializeField] private string gameWord;
    [SerializeField] private List<string> anagramsList; //might not be necessary;  we can just check if the inputWord is an anagram of the base word -- also check if its a real word
    [SerializeField] private Dictionary<char, int> pointValues;

    [Header("Live Data")]
    [SerializeField] private int totalPoints;
    [SerializeField] private List<string> wordsUsed;
    [SerializeField] private TextMeshProUGUI scoreBox;
    [SerializeField] private TextMeshProUGUI wordDisplay;
    [SerializeField] private GameObject gameTimer;
    [SerializeField] private SubmitManager submitManager;

    [Header("Animation Stuffs")]
    [SerializeField] private int transitionDuration;

    [SerializeField] private GameObject GameAlertPrefab;

    private Random rand = new Random();

    private List<string> words = new List<string> { "lemons", "anagram", "tenacious", "repristinate", "superannuated", "rasorial", "lambent", "caravel", "gridiron", "quixotic", "bulwark", "syzygy", "vellicate", "rejuvenate", "retcon", "paresthesia", "pandiculation", "leveret" };

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(transitionDuration);
        yield return Countdown();
        InitializeGame();
        yield return new WaitForEndOfFrame();
    }

    public void SetLevelID(int ID)
    {
        levelID = ID;
    }

    //initialize all variables and signal the start of the course with a selected word
    private void InitializeGame()
    {
        totalPoints = 0;
        if (levelID > 0)
        {
            gameWord = words[levelID - 1].ToUpper();
        }
        wordDisplay.text = gameWord;
        pointValues = new Dictionary<char, int>()
        {
            {'A', 1}, {'E', 1}, {'I', 1}, {'L', 1}, {'N', 1}, {'O', 1}, {'R', 1}, {'S', 1}, {'T', 1}, {'U', 1},
            {'D', 2}, {'G', 2},
            {'B', 3}, {'C', 3}, {'M', 3}, {'P', 3},
            {'F', 4}, {'H', 4}, {'V', 4}, {'W', 4}, {'Y', 4},
            {'K', 5},
            {'J', 8}, {'X', 8},
            {'Q', 10}, {'Z', 10},
        };

        Debug.Log($"Game has begun with the word: {gameWord}");
        wordsUsed.Add(gameWord);
    }

    public bool submitWord(string word)
    {
        Debug.Log($"attempting to use word: {word}");
        Debug.Log($"Is {word} an anagram?: {isAnagram(word)}");

        Debug.Log($"Is {word} in wordsUsed?: {wordsUsed.Contains(word)}");

        if (word.Length > 2 && isAnagram(word) && !wordsUsed.Contains(word)) //later, check if its an actual word in the dictionary
        {

            //true -> calculate points earned and increase totalPoints
            int pointsReceived = 0;
            wordsUsed.Add(word);
            foreach (char a in word.ToCharArray())
            {
                pointsReceived += pointValues[a];
            }

            totalPoints += pointsReceived;
            scoreBox.GetComponent<TextMeshProUGUI>().text = $"Score: {totalPoints}";
            Debug.Log($"Points received for word \"{word}\": {pointsReceived} points.");
            Debug.Log($"Total points now: {totalPoints}");

            DisplayAlert(rand.NextDouble() + "Success", $"+{pointsReceived} for {word}", 0.2f, 1f, 100, 0.3f);
            StartCoroutine(submitManager.FadeOutCR(new Color(0.09803922f, .6666667f, 0, 1)));

            return true;
        }
        else
        {
            //false, output error msg --> need to define specific errors later
            Debug.Log($"\"{word}\" is not a valid word/has already been used.");

            DisplayAlert(rand.NextDouble() + "Fail", $"{word} is invalid.", 0.2f, 2f, 100, 0.3f);
            StartCoroutine(submitManager.FadeOutCR(new Color(1, 0, 0.0361886f, 1)));

            return false;
        }
    }

    public void SetWord(string word)
    {
        gameWord = word;
        Debug.Log($"gameWord has been updated to: \"{word}\".");
    }

    private bool isAnagram(string inputWord)
    {
        var inputArray = inputWord.ToCharArray().ToList();
        var baseArray = gameWord.ToCharArray().ToList();
        int matches = 0;

        baseArray.Sort((char a, char b) =>
        {
            return b.CompareTo(a);
        });

        foreach (char character in inputArray)
        {
            if (baseArray.IndexOf(character) >= 0)
            {
                matches++;
                baseArray.Remove(character);
            }
        }
        //sees if the number of character matches is equivalent to all the characters in the inputWord
        return matches == inputWord.Length;
    }

    public WaitForSeconds DisplayAlert(String name, String text, float duration1, float duration2, int fontSize, float inBetween = 0)
    {
        //Vector2 size = new Vector2(Screen.width * 0.8f, Screen.height * 0.1f);
        //Vector2 position = GameObject.Find("Game Alert").transform.position;

        GameObject gameAlert = Instantiate(GameAlertPrefab, GameObject.Find("Game Alert").transform);
        gameAlert.name = name;

        gameAlert.GetComponent<TextMeshProUGUI>().fontSize = fontSize;
        gameAlert.GetComponent<TextMeshProUGUI>().text = text;

        gameAlert.GetComponent<GameAlert>().thisAlert = GameObject.Find(name);
        gameAlert.GetComponent<GameAlert>().duration1 = duration1;
        gameAlert.GetComponent<GameAlert>().duration2 = duration2;
        gameAlert.GetComponent<GameAlert>().inBetween = inBetween;
        return new WaitForSeconds(duration1 + duration2 + inBetween);
    }

    public IEnumerator Countdown()
    {
        GameObject BackgroundBlur = GameObject.Find("Canvas - Blur");
        GameObject GameAlert = GameObject.Find("Game Alert");

        GameAlert.transform.SetParent(BackgroundBlur.transform);
        GameAlert.transform.SetAsLastSibling();

        yield return DisplayAlert("3", "3", 0.7f, 0.1f, 250, 0.3f);
        yield return DisplayAlert("2", "2", 0.7f, 0.1f, 250, 0.3f);
        yield return DisplayAlert("1", "1", 0.7f, 0.1f, 250, 0.3f);
        yield return DisplayAlert("Start", "Start!", 0.5f, 0.4f, 250, 0.3f);

        GameAlert.transform.transform.SetParent(GameObject.Find("GUICanvas").transform);
        BackgroundBlur.SetActive(false);
        gameTimer.GetComponent<GameTimer>().TogglePause();
    }

    public string GetGameWord()
    {
        return gameWord;
    }

    public int GetFinalScore()
    {
        return totalPoints;
    }

    /*public int GetHighScore()
    {
        return highScore;
    }*/

    public List<string> GetWordsUsed()
    {
        return wordsUsed;
    }

    public int GetLevelID()
    {
        return levelID;
    }
}
