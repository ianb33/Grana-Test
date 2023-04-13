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
                tempPlayer p = new tempPlayer
                {
                    playerName = player.user.username,
                    playerUUID = player.user.username,
                    highScore = player.score
                };



            }

            //Sort list by high score
            playerList = playerList.OrderByDescending(o => o.highScore).ToList();

            GetPlayerIndex();
            //Only generate list if leaderboard was successfully loaded.
            GenerateList();
        }
        else //failed to load leaderboard;

            Debug.Log("Finished running GetLeaderboard request.");
    }

    private void GetPlayerIndex()
    {
        string UUID = PlayerPrefs.GetString("PlayerID");
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].playerUUID == UUID)
            {
                currentPlayerIndex = i;
                break;
            }
        }
    }

    private void GenerateList()
    {
        //set position and height
        RectTransform contentRect = GameObject.Find("Content").GetComponent<RectTransform>();
        int top = currentPlayerIndex * 70;
        int bottom = (playerList.Count - currentPlayerIndex) * -70;

        contentRect.offsetMax = new Vector2(contentRect.offsetMax.x, top);
        contentRect.offsetMin = new Vector2(contentRect.offsetMin.x, bottom);


        for (int i = 0; i < playerList.Count; i++)
        {
            GameObject cell = Instantiate(LBCellPrefab, GameObject.Find("Content").transform);
            cell.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1) + "  " + playerList[i].playerName;
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
