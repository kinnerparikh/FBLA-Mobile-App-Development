using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class EndController : MonoBehaviour
{
    // Serialized Fields
    [SerializeField]
    private GameObject scoreText;
    [SerializeField]
    public GameObject highScore;
    [SerializeField]
    TMP_Text nameText;

    public Button m_firstButton;

    // URL for leaderboards
    const string webUrl = "http://dreamlo.com/lb/fSJ0kZGVv0mxWHfmsF6fqASwUXyLPUE0KRhYaWOUzJ7Q";

    void Start()
    {
        // Username text on screen
        nameText.text = "Name: " + MenuController.username;
        // Update score on screen
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + GameController.playerScore;
        // Calls method to upload score to global leaderboards
        StartCoroutine(UploadNewHighScore(MenuController.username, (int)GameController.playerScore));
        // Calls method to update local high score
        HighScore((int)GameController.playerScore);

        m_firstButton.onClick.AddListener(ResetGame);
        FindObjectOfType<MusicManager>().Stop("Gameplay");
        FindObjectOfType<MusicManager>().Stop("ThemeSong");
        FindObjectOfType<MusicManager>().Play("ThemeSong");

        // Set score back to 0 for the new game 
        GameController.numCorrect = 0;
        GameController.playerScore = 0;
    }

    // Loads leaderboard scene 
    public void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    // Resets game after a loss or after 10 questions answered correctly
    void ResetGame()
    {
        // Load start screen
        SceneManager.LoadScene("Start");

        //Reset question database
        for (int i = 0; i < Questions.questionsUsed.Count; i++)
        {
            Questions.questionsUsed[i].Used = false;
        }
    }

    // Share button action controller
    public void Share()
    {
        NativeShare ns = new NativeShare();
        ns.SetTitle("Check Out This AWESOME Game");
        ns.SetText("I Just GOT " + GameController.numCorrect + " on the Fast Facts: FBLA QUIZ!!\n" +
            "https://github.com/kinzorPark/FBLA-Mobile-App-Development");
        ns.Share();
    }

    // Local high score updater
    public void HighScore(int currScore)
    {
        // If the current score in higher than the stored high score
        if (currScore > PlayerPrefs.GetInt("highScore", 0))
        {
            // Debugging log
            Debug.Log("High Score updated from " + PlayerPrefs.GetInt("highScore", 0) + " to " + currScore);

            // Update the local high score
            PlayerPrefs.SetInt("highScore", currScore);
        }

        // Debugging log
        else
        {
            Debug.Log("High Score stays same: " + PlayerPrefs.GetInt("highScore", 0));
        }

        // Update text on UI
        highScore.GetComponent<TextMeshProUGUI>().text = "High Score: " + PlayerPrefs.GetInt("highScore", 0);



    }

    // Uploads new high score to online leaderboards
    IEnumerator UploadNewHighScore(string username, int score)
    {
        // Debugging logs
        Debug.Log("UploadNewHighScoreRun");
        Debug.Log(webUrl + "/add/" + WWW.EscapeURL(username) + "/" + score);

        // Creates new object of type "WWW"
        WWW www = new WWW(webUrl + "/add/" + WWW.EscapeURL(username) + "/" + score);
        // Returns "WWW" object
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Upload successful");
        }
        // Debugging log
        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Upload successful");
        }

        // Debugging log
        else
        {
            Debug.Log("Upload failed");
        }
    }
}
