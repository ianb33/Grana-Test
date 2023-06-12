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
    [Header("Live Data")]

    [SerializeField] private TextMeshProUGUI scoreBox;
    [SerializeField] private TextMeshProUGUI wordDisplay;
    [SerializeField] private GameObject gameTimer;
    [SerializeField] private SubmitManager submitManager;

    [Header("Animation Stuffs")]
    [SerializeField] private int transitionDuration;

    [SerializeField] private GameObject GameAlertPrefab;

    private Random rand = new Random();

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(transitionDuration);
        yield return Countdown();
        InitializeGame();
        yield return new WaitForEndOfFrame();
    }



    //initialize all variables and signal the start of the course with a selected word
    private void InitializeGame()
    {
        LevelData.levelData.totalPoints = 0;
        if (LevelData.levelData.levelID > 0)
        {
            LevelData.levelData.gameWord = LevelData.levelData.words[LevelData.levelData.levelID - 1].ToUpper(); //will need to be changed when words are dynamic
        }
        wordDisplay.text = LevelData.levelData.gameWord;

        Debug.Log($"Game has begun with the word: {LevelData.levelData.gameWord}");
        LevelData.levelData.wordsUsed.Add(LevelData.levelData.gameWord);
    }

    public bool submitWord(string word)
    {
        Debug.Log($"attempting to use word: {word}");
        Debug.Log($"Is {word} an anagram?: {isAnagram(word)}");

        Debug.Log($"Is {word} in wordsUsed?: {LevelData.levelData.wordsUsed.Contains(word)}");

        if (word.Length > 2 && isAnagram(word) && !LevelData.levelData.wordsUsed.Contains(word)) //later, check if its an actual word in the dictionary
        {

            //true -> calculate points earned and increase totalPoints
            int pointsReceived = 0;
            LevelData.levelData.wordsUsed.Add(word);
            foreach (char a in word.ToCharArray())
            {
                pointsReceived += LevelData.levelData.pointValues[a];
            }

            LevelData.levelData.totalPoints += pointsReceived;
            scoreBox.GetComponent<TextMeshProUGUI>().text = $"Score: {LevelData.levelData.totalPoints}";
            Debug.Log($"Points received for word \"{word}\": {pointsReceived} points.");
            Debug.Log($"Total points now: {LevelData.levelData.totalPoints}");

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

    private bool isAnagram(string inputWord)
    {
        var inputArray = inputWord.ToCharArray().ToList();
        var baseArray = LevelData.levelData.gameWord.ToCharArray().ToList();
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
}
