using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{

    [SerializeField] private BackendManager BackendManager;
    
    private string PlayerID;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerID  = SystemInfo.deviceUniqueIdentifier;
        SignIn();
        
    }

    // Logs the player into their device-specific account
    private async void SignIn()
    {
        //Set UUID for the specified device
        PlayerPrefs.SetString("PlayerID", PlayerID);
        
        //Format WWWForm to acquire access token
        WWWForm SigninRequest = new WWWForm();
        SigninRequest.AddField("username", PlayerID);
        SigninRequest.AddField("password", PlayerID);

        string requestResult = await BackendManager.POSTRequest("https://grana.vinniehat.com/api/auth/signin", SigninRequest, false);
        if (requestResult != null)
        {
            var x = JsonConvert.DeserializeObject<SignInModel>(requestResult);
            PlayerPrefs.SetString("accessToken", x.accessToken);
            Debug.Log("accessToken: " + x.accessToken);
        }
        else SignUp();
        
        Debug.Log("Completed SignIn request.");

    }

    private async void SignUp()
    {
        //Set UUID for the specified device
        PlayerPrefs.SetString("PlayerID", PlayerID);
        
        //Format WWWForm to input login info
        WWWForm SignupRequest = new WWWForm();
        SignupRequest.AddField("username", PlayerID);
        SignupRequest.AddField("password", PlayerID);
        SignupRequest.AddField("email", PlayerID);
        
        //Send the form to the backend API
        string requestResult = await BackendManager.POSTRequest("https://grana.vinniehat.com/api/auth/signup", SignupRequest, false);

        Debug.Log("Completed SignUp request.");
    }
    
}

//level ID and score
