using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class BackendManager : MonoBehaviour
{

    public async Task<string> GETRequest(string url)
    {
        // Send the HTTP request asynchronously
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
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
                Debug.Log($"Failed to Pulled From {url}");
                return null;
            }
            else
            {
                // Get the response body
                string responseBody = webRequest.downloadHandler.text;

                // Do something with the response body
                Debug.Log($"Successfully Pulled From {url}");
                Debug.Log(responseBody);
                return responseBody;

            }

        }
    }

    public async Task<string> POSTRequest(string url, string jsonRequestBody)
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
                Debug.Log($"Failed to Send to {url}");
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
                Debug.Log($"Failed to Update {url}");
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

