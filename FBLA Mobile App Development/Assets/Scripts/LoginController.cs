using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;
using Facebook.MiniJSON;

public class LoginController : MonoBehaviour
{
    public void PlayAsGuest()
    {
        // Load the Scene to choose topic
        SceneManager.LoadScene("NameEntry");
    }

    public void Login()
    {
        var permissions = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(permissions, AuthorizeCallback);
    }


    private void AuthorizeCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            FB.API("me?fields=name", HttpMethod.GET, NameCallBack);

            SceneManager.LoadScene("Start");

        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }


void NameCallBack(IResult result)
    {
        if (result.Error == null)
        {
            MenuController.username = result.ResultDictionary["name"].ToString();
        }
        else
        {
            Debug.Log(result.Error);
        }

    }

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            //initialize FB SDK
            FB.Init();
        }
        else
        {
            //If FB is initialized, signal app activiation 
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // App activation
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Cannot Initialize Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game
            Time.timeScale = 0;
        }
        else
        {
            // Resume the gamea
            Time.timeScale = 1;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
