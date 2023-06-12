using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData levelData { get; private set; }

    public int levelID { get; set; }
    public string gameWord { get; set; }
    public List<string> words = new List<string> { "lemons", "anagram", "tenacious", "repristinate", "superannuated", "rasorial", "lambent", "caravel", "gridiron", "quixotic", "bulwark", "syzygy", "vellicate", "rejuvenate", "retcon", "paresthesia", "pandiculation", "leveret" };

    public int totalPoints { get; set; }
    public List<string> wordsUsed;
    public Dictionary<char, int> pointValues = new Dictionary<char, int>()
    {
        {'A', 1}, {'E', 1}, {'I', 1}, {'L', 1}, {'N', 1}, {'O', 1}, {'R', 1}, {'S', 1}, {'T', 1}, {'U', 1},
        {'D', 2}, {'G', 2},
        {'B', 3}, {'C', 3}, {'M', 3}, {'P', 3},
        {'F', 4}, {'H', 4}, {'V', 4}, {'W', 4}, {'Y', 4},
        {'K', 5},
        {'J', 8}, {'X', 8},
        {'Q', 10}, {'Z', 10},
    };

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (levelData != null && levelData != this)
        {
            Debug.Log("destroy" + this);
            Destroy(this);
        }
        else
        {
            Debug.Log("setting leveldata" + this);
            levelData = this;
        }
    }
}
