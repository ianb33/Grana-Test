using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{

    [SerializeField] private BackendManager BackendManager;
    // Start is called before the first frame update
    void Start()
    {
        LogIn();
    }

    // Logs the player into their device-specific account
    private void LogIn()
    {
        //Set UUID for the specified device
        string PlayerID = SystemInfo.deviceUniqueIdentifier;
        PlayerPrefs.SetString("PlayerID", PlayerID);
        
        //Format WWWForm to acquire access token
        WWWForm LoginRequest = new WWWForm();
        LoginRequest.AddField("username", PlayerID);
        LoginRequest.AddField("password", PlayerID);

        Debug.Log(BackendManager.POSTRequest("https://grana.vinniehat.com/api/auth/signup", LoginRequest, false));
    }
}
