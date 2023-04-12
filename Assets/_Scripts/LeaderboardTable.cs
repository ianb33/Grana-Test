using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardTable : MonoBehaviour
{
    [SerializeField] private LeaderboardManager LeaderboardManager;
    [SerializeField] private GameObject LBCellPrefab;

    //temporary, will be derived from current player information.
    [SerializeField] private int currentPlayerIndex;

    //List of players and their scores will be sorted before being pulled from the backend.
    [SerializeField] private List<tempPlayer> playerList = new List<tempPlayer>();

    private void Start()
    {
        GetLeaderboard();
    }

   
    private async void GetLeaderboard()
    {
        //Format WWWForm to acquire score data
        WWWForm ScoresRequest = new WWWForm();
        ScoresRequest.AddField("levelID", LeaderboardManager.LevelID);

        string requestResult = await BackendManager.GETRequest($"https://grana.vinniehat.com/api/score/scores/{LeaderboardManager.LevelID}");
        if (requestResult != null)
        {
            //Deserialize score data and convert to List<tempPlayer>
            var x = JsonConvert.DeserializeObject<List<LeaderboardModel>>(requestResult);
            foreach (LeaderboardModel player in x)
            {
                playerList.Add(new tempPlayer
                {
                    playerName = player.user.username,
                    highScore = player.score
                });
                
                
                
            }

            //Only generate list if leaderboard was successfully loaded.
            GenerateList();
        }
        else //failed to load leaderboard;
        
        Debug.Log("Finished running GetLeaderboard request.");
    }
    
    private void GenerateList()
    {
        //set position and height
        RectTransform contentRect = GameObject.Find("Content").GetComponent<RectTransform>();
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, Screen.width / 8 * playerList.Count);
        contentRect.position = new Vector2(contentRect.position.x, -Screen.width / 8 * currentPlayerIndex);
        

        for (int i = 0; i < playerList.Count; i++)
        {
            GameObject cell = Instantiate(LBCellPrefab, GameObject.Find("Content").transform);
            cell.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i+1) + "  " + playerList[i].playerName;
            cell.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerList[i].highScore + "";

            if (i == currentPlayerIndex)
            {
                cell.GetComponent<Image>().color = new Color(1f, 0.55f, 0.2f);
            }
        }
    }

    private void GetPlayerIndex()
    {
        
    }
    
    private class tempPlayer
    {
        public string playerName { get; set; }
        public int highScore { get; set; }
    }
}
