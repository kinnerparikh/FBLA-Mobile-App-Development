using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class LeaderBoardsController : MonoBehaviour
{
    //High score text 
    public TMP_Text leaderboardText;
    const string webUrl = "http://dreamlo.com/lb/fSJ0kZGVv0mxWHfmsF6fqASwUXyLPUE0KRhYaWOUzJ7Q";
    public Highscore[] highscoreList;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownloadHighScoreFromDatabase());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DownloadHighScoreFromDatabase()
    {
        WWW www = new WWW(webUrl + "/pipe/");
        yield return www;

        Debug.Log(www.text);
        if (string.IsNullOrEmpty(www.error))
            FormatHighScores(www.text);

        

        string scoreString = "";
        for (int i = 0; i < Mathf.Min(highscoreList.Length, 10); i++)
        {
            string str = highscoreList[i].getUsername() + " - " + highscoreList[i].getScore() + "\n";
            StringBuilder newStringB = new StringBuilder(str);
            newStringB.Replace("+", " ");

            scoreString += newStringB.ToString();
        }

        Debug.Log(scoreString);
        leaderboardText.text = scoreString;

    }

    void FormatHighScores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoreList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split('|');
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoreList[i] = new Highscore(username, score);

            Debug.Log(highscoreList[i].username + ": " + highscoreList[i].score);
        }
    }
}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }

    public int getScore()
    {
        return score;
    }

    public string getUsername()
    {
        return username;
    }
}
