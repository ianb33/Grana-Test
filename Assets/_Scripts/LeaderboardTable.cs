using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardTable : MonoBehaviour
{
    [SerializeField] private LeaderboardManager LeaderboardManager;
    [SerializeField] private GameObject LBCellPrefab;
    [SerializeField] private GameObject ErrorMessage;


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
        //Format WWWForm to acquire score data.
        WWWForm scoresRequest = new WWWForm();
        scoresRequest.AddField("levelID", LeaderboardManager.LevelID);

        //Send GET request to the backend API and acquire score list.
        string requestResult = await BackendManager.GETRequest($"https://grana.vinniehat.com/api/score/scores/{LeaderboardManager.LevelID}");
        if (requestResult != null)
        {
            //Deserialize score data and convert to List<tempPlayer>.
            var x = JsonConvert.DeserializeObject<List<LeaderboardModel>>(requestResult);
            string userID = PlayerPrefs.GetString("PlayerID");
            
            //Transfer data into playerList for rendering.
            foreach (LeaderboardModel player in x)
            {
                tempPlayer a = new tempPlayer
                {
                    playerName = player.user.username,
                    playerUUID = player.user.uuid,
                    highScore = player.score
                };
                a.SetDisplayName();
                playerList.Add(a);

            }
            
            //Sort the list by score.
            playerList.OrderByDescending(o => o.highScore);
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].playerUUID == userID)
                {
                    currentPlayerIndex = i + 1;
                    Debug.Log($"Current player's index: {currentPlayerIndex}");
                }
            }

            //Sort list by high score
            playerList = playerList.OrderByDescending(o => o.highScore).ToList();

            //Only generate list if leaderboard was successfully loaded.
            GetPlayerIndex();
            GenerateList();
        }
        else
        {
            //Failed to load leaderboard, do not load prefab. 
            Debug.Log("Failed to load the leaderboard.");
            

        }
        Debug.Log("Finished running GetLeaderboard request.");
    }

    private void GetPlayerIndex()
    {
        string UUID = PlayerPrefs.GetString("PlayerID");
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].playerUUID == UUID || playerList[i].playerName == UUID)
            {
                currentPlayerIndex = i;
                Debug.Log($"Current Player Index: {currentPlayerIndex}");
                break;
            }
        }
    }

    private void GenerateList()
    {
        //set position and height
        RectTransform contentRect = GameObject.Find("Content").GetComponent<RectTransform>();

        contentRect.position = new Vector2(contentRect.position.x, currentPlayerIndex * Screen.height / 12);
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, playerList.Count * Screen.height / 12);


        for (int i = 0; i < playerList.Count; i++)
        {
            GameObject cell = Instantiate(LBCellPrefab, GameObject.Find("Content").transform);
            cell.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i+1) + "  " + playerList[i].displayName;
            cell.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = playerList[i].highScore + "";

            if (i == currentPlayerIndex)
            {
                cell.GetComponent<Image>().color = new Color(1f, 0.55f, 0.2f);
            }
        }
    }
    private class tempPlayer
    {
        public string displayName = "";
        public string playerName { get; set; }
        public int highScore { get; set; }
        public string playerUUID { get; set; }

        public void SetDisplayName()
        {
            displayName = "User" + playerName.Substring(0, 4);
        }
    }
}
