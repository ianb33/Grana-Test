using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    [SerializeField] private GameObject loginMenu;
    [SerializeField] private GameObject registerMenu;
    [SerializeField] private GameObject accountMenu;

    public bool loggedIn = false;

    public void Start()
    {

    }

    public void LoginButtonClicked()
    {
        Debug.Log("Login button clicked");
        if(loggedIn) {
            accountMenu.SetActive(true);
            loginMenu.SetActive(false);
            registerMenu.SetActive(false);
        }
        else 
        {
            loginMenu.SetActive(true);
            registerMenu.SetActive(false);
            accountMenu.SetActive(false);
        }
    }

    public void CloseLoginMenu()
    {
        loginMenu.SetActive(false);
    }

    public void CloseRegisterMenu() 
    {
        registerMenu.SetActive(false);
    }

    public void CloseAccountMenu() 
    {
        accountMenu.SetActive(false);
    }
}
