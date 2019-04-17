using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Facebook.Unity;

//static bool AudioBegin = false;

public class MenuController : MonoBehaviour
{
    /* ===========================
    * Question Database Class
    * ===========================
    * This class controls the start scene
    */

    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private GameObject instructionsButton;
    [SerializeField]
    private GameObject creditsButton;
    [SerializeField]
    private GameObject bugMenu;

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
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    private void Awake()
    {
        if(!FB.IsInitialized)
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

    public void Start()
    {
        // Always run the following in order

        // Imports questions to lists of strings from csv
        QuestionDatabase.ImportGame("final");
        // Load all the strings into lists of Question Set Objects organized by categories
        Questions.LoadAllQuestions();

        
        FindObjectOfType<MusicManager>().Stop("Gameplay");
        FindObjectOfType<MusicManager>().Stop("ThemeSong");
        FindObjectOfType<MusicManager>().Play("ThemeSong");

    }
    // Called when Start Button is Pressed
    public void StartGame()
    {
        // Load the Scene to choose topic
        SceneManager.LoadScene("Topic");
    }

    // Called when Instructions button is Pressed
    public void Instructions()
    {
        // Open Instructions Scene
        SceneManager.LoadScene("Instructions");
    }

    // Called when Credits button is Pressed
    public void Credits()
    {
        // Open Credits Scene
        SceneManager.LoadScene("Credits");
    }

    private void Update()
    {
        if (bugMenu.activeSelf == true)
        {
            startButton.GetComponent<Button>().enabled = false;
            creditsButton.GetComponent<Button>().enabled = false;
            instructionsButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            startButton.GetComponent<Button>().enabled = true;
            creditsButton.GetComponent<Button>().enabled = true;
            instructionsButton.GetComponent<Button>().enabled = true;
        }
    }

    /*public class menuMusic : MonoBehaviour
    {
        private void Awake()
        {
            if (!AudioBegin)

        }
    }*/
}
