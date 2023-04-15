using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{

    [SerializeField] public int LevelID;
    [SerializeField] private string GameWord;
    [SerializeField] private TextMeshProUGUI Subtitle;
    private List<string> words = new List<string> { "lemons", "anagram", "tenacious", "repristinate", "superannuated", "rasorial", "lambent", "caravel", "gridiron", "quixotic", "bulwark", "syzygy", "vellicate", "rejuvenate", "retcon", "paresthesia", "pandiculation", "leveret" };

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        SetScreenData();
    }

    public void SetLevelID(int levelID)
    {
        LevelID = levelID;
    }

    public void SetGameWord(string gameWord)
    {
        GameWord = gameWord;
    }

    private void SetScreenData()
    {
        Subtitle.text = $"Level {LevelID} : {GameWord}";
    }
}
