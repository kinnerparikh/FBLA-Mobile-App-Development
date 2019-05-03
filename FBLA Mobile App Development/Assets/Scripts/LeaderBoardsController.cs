using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class LeaderBoardsController : MonoBehaviour
{
    // High score text 
    public TMP_Text leaderboardText;
    // Online leaderboard URL
    const string webUrl = "http://dreamlo.com/lb/fSJ0kZGVv0mxWHfmsF6fqASwUXyLPUE0KRhYaWOUzJ7Q";
    public HighScore[] highScoreList;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownloadHighScoreFromDatabase());

        /*// Update leaderboard with high scores from HighScore class
        List<Score> scores = HighScores.GetScores();
        Debug.Log(scores.Count);
        string scoresString = "";
        for(int i = 0; i < Mathf.Min(scores.Count, 10); i++)
        {
            scoresString += scores[i].Name + " - " + scores[i].Val + "\n";
        }
        Debug.Log(scoresString);
        leaderboardText.text = scoresString;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Downloading the scores from the online leaderboards
    IEnumerator DownloadHighScoreFromDatabase()
    {
        // Creates new "WWW" object 
        // webURL + "/pipe/" returns a comma separated string
        WWW www = new WWW(webUrl + "/pipe/");
        // Returns the "WWW" oject
        yield return www;

        // Debugging log
        Debug.Log(www.text);

        // If there is no error with the "WWW" object
        if (string.IsNullOrEmpty(www.error))
        {
            // Call "FormatHighScores"
            FormatHighScores(www.text);
        }

        // String to set the UI object to
        string scoreString = "";

        // Parsing through "highScoreList"
        for (int i = 0; i < Mathf.Min(highScoreList.Length, 10); i++)
        {
            // creating a string with the username and score with formatting
            string tempStr = highScoreList[i].getUsername() + " - " + highScoreList[i].getScore() + "\n";
            // Creating a new "StringBuilder" object from "tempStr"
            StringBuilder newStringB = new StringBuilder(tempStr);
            // Replacing any "+" (plus) with a " " (space)
            newStringB.Replace("+", " ");

            // Add modified StringBuilder back to "scoreString"
            scoreString += newStringB.ToString();
        }

        // Debugging Log
        Debug.Log(scoreString);
        // Set the UI text to "scoreString"
        leaderboardText.text = scoreString;
    }

    // Parses through recieved data and creates array of "HighScore" objects out of them
    void FormatHighScores(string textStream)
    {
        // Creates new string array by splitting the recieved data
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        // Initializing highScoreList array
        highScoreList = new HighScore[entries.Length];

        // Parsing through "entries" array
        for (int i = 0; i < entries.Length; i++)
        {
            // Split the entry every "|" and create an array of the split parts
            string[] entryInfo = entries[i].Split('|');
            // Set username to the first value in "entryInfo" array
            string tempUsername = entryInfo[0];
            // Creates the second value in "entryInfo" array to an int and sets that to tempScore
            int tempScore = int.Parse(entryInfo[1]);

            // Creates a new "HighScore" object in the highScoreList
            highScoreList[i] = new HighScore(tempUsername, tempScore);

            // Debugging log
            Debug.Log(highScoreList[i].username + ": " + highScoreList[i].score);
        }
    }
}

// HighScore class
public struct HighScore
{
    // Declarations
    public string username;
    public int score;

    // Constructor
    public HighScore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }

    // Returns the score
    public int getScore()
    {
        return score;
    }

    // Returns the username
    public string getUsername()
    {
        return username;
    }
}
