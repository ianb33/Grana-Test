using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using TMPro;

public class AccountInfoManager : MonoBehaviour
{
    public TextMeshProUGUI usernameText;
    // Start is called before the first frame update
    void Start()
    {
        SetUsername(FirebaseAuth.DefaultInstance.CurrentUser.DisplayName);
    }

    public void SetUsername(string username)
    {
        usernameText.text += username;
        Debug.Log("Username set to: " + username);
    }
}
