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
    void Start()
    {
        if (LevelID > 0) { GameWord = words[LevelID - 1].ToUpper();}
        Subtitle.text = $"Level {LevelID}: \"{GameWord}\"";
    }

}
