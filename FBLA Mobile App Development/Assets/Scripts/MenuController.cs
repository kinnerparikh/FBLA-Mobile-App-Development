using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    /* ===========================
    * Question Database Class
    * ===========================
    * This class controls the start scene
    */
    public void Start()
    {
        // Always run the following in order

        // Imports questions to lists of strings from csv
        QuestionDatabase.ImportGame("Test");
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
}
