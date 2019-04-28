using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderBoardsController : MonoBehaviour
{
    //High score text 
    public TMP_Text leaderboardText;

    // Start is called before the first frame update
    void Start()
    {
        // Update leaderboard with high scores from HighScore class
        List<Score> scores = HighScores.GetScores();
        Debug.Log(scores.Count);
        string scoresString = "";
        for(int i = 0; i < Mathf.Min(scores.Count, 10); i++)
        {
            scoresString += scores[i].Name + " - " + scores[i].Val + "\n";
        }
        Debug.Log(scoresString);
        leaderboardText.text = scoresString;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
