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
        string PlayerID = SystemInfo.deviceUniqueIdentifier;
        PlayerPrefs.SetString("PlayerID", PlayerID);
        BackendManager.POSTRequest("", PlayerID);
    }
}
