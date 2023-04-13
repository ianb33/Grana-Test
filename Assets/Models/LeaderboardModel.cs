using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardModel
{
    public int scoreID { get; set; }
    public int levelID { get; set; }
    public int score { get; set; }
    public string createdAt { get; set; }
    public string updatedAt { get; set; }
    public int userID { get; set; }
    public UserModel user { get; set; } 
}

public class UserModel
{
    public int userID { get; set; }
    public string uuid { get; set; }
    public string username { get; set; }

}
