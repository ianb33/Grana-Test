using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

public class BackendManager : MonoBehaviour
{

    public static async Task<string> GETRequest(string url, bool requiresAuth = true)
    {
        
        
        // Send the HTTP request asynchronously
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            if (requiresAuth)
            {
                string accessToken = PlayerPrefs.GetString("accessToken");
                Debug.Log($"Using token: {accessToken}");
                webRequest.SetRequestHeader("x-access-token", accessToken);
            }

            UnityWebRequestAsyncOperation asyncOp = webRequest.SendWebRequest();

            // Wait for the request to complete
            while (!asyncOp.isDone)
            {
                await Task.Delay(1000 / 60);
            }

            // Check for errors
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"BackendManager: {webRequest.error}");
                Debug.Log($"Failed to GET From {url}");
                return null;
            }
            else
            {
                // Get the response body
                string responseBody = webRequest.downloadHandler.text;  

                // Do something with the response body
                Debug.Log($"Successfully got data From {url}");
                Debug.Log($"BackendManager: {responseBody}");
                return responseBody;

            }

        }
    }

    public async Task<string> POSTRequest(string url, WWWForm RequestBody, bool requiresAuth = true)
    {
        Debug.Log($"RequestBody: {RequestBody}");
        
        // Send the HTTP request asynchronously
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, RequestBody))
        {
            if (requiresAuth)
            {
                string accessToken = PlayerPrefs.GetString("accessToken");
                Debug.Log($"Using token: {accessToken}");
                webRequest.SetRequestHeader("x-access-token", accessToken);
            }
            
            UnityWebRequestAsyncOperation asyncOp = webRequest.SendWebRequest();

            // Wait for the request to complete
            while (!asyncOp.isDone)
            {
                await Task.Delay(1000 / 60);
            }

            // Check for errors
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
                Debug.Log($"Failed to POST to {url}");
                return null;
            }
            else
            {
                // Get the response body
                string responseBody = webRequest.downloadHandler.text;

                // Do something with the response body
                Debug.Log($"Successfully Posted To {url}");
                Debug.Log(responseBody);
                return responseBody;

            }

        }
    }
    
    public async Task<string> PUTRequest(string url, string jsonRequestBody)
    {
        // Send the HTTP request asynchronously
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, jsonRequestBody))
        {
            
            webRequest.SetRequestHeader("Content-Type", "application/json");
            UnityWebRequestAsyncOperation asyncOp = webRequest.SendWebRequest();

            // Wait for the request to complete
            while (!asyncOp.isDone)
            {
                await Task.Delay(1000 / 60);
            }

            // Check for errors
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
                Debug.Log($"Failed to PUT to {url}");
                return null;
            }
            else
            {
                // Get the response body
                string responseBody = webRequest.downloadHandler.text;

                // Do something with the response body
                Debug.Log($"Successfully Updated {url}");
                Debug.Log(responseBody);
                return responseBody;

            }

        }
    }
}

