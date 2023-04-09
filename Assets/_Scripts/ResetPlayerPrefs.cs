using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ResetPlayerPrefs : MonoBehaviour
{

    [SerializeField] private BackendManager BackendManager;
    // Start is called before the first frame update
    void Start()
    {
        SignUp();
    }

    // Logs the player into their device-specific account
    private async void SignIn()
    {
        //Set UUID for the specified device
        string PlayerID = SystemInfo.deviceUniqueIdentifier;
        PlayerPrefs.SetString("PlayerID", PlayerID);
        
        //Format WWWForm to acquire access token
        WWWForm SigninRequest = new WWWForm();
        SigninRequest.AddField("username", PlayerID);
        SigninRequest.AddField("password", PlayerID);

        string requestResult = await BackendManager.POSTRequest("https://grana.vinniehat.com/api/auth/signup", SigninRequest, false);
        if(requestResult != null) PlayerPrefs.SetString("AccessToken", requestResult);
        
    }

    private async void SignUp()
    {
        //Set UUID for the specified device
        string PlayerID = SystemInfo.deviceUniqueIdentifier;
        PlayerPrefs.SetString("PlayerID", PlayerID);
        
        //Format WWWForm to acquire access token
        WWWForm SignupRequest = new WWWForm();
        SignupRequest.AddField("username", PlayerID);
        SignupRequest.AddField("password", PlayerID);
        SignupRequest.AddField("email", PlayerID);

        string requestResult = await BackendManager.POSTRequest("https://grana.vinniehat.com/api/auth/signup", SignupRequest, false);
        if(requestResult != null) PlayerPrefs.SetString("AccessToken", requestResult);
    }
}
