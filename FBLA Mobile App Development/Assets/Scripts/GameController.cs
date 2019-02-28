using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /* ===========================
    * Game Controller Class
    * ===========================
    * 
    * Controller class controls how the game is ran in unity and sets everything up
    * 
    */

    // Total amount of questions (Change in inspector)
    [SerializeField]
    private int totalQuestions = 10;


    // Start of program
    void Start()
    {
        // Always run the following in order

        // Imports questions to lists of strings from csv
        QuestionDatabase.ImportGame("stuff.csv");
        // Set the total amount of questions for the RNG
        Questions.SetTotalQuestions(totalQuestions);
        // Load all the strings into lists of Question Set Objects organized by categories
        Questions.LoadAllQuestions();
    }

    // Update is called once per frame
    void Update()
    {
        // To Use
        // Create new question set and call function Questions.GetQuestion(Category) to get a random question from that category
        QuestionSet q1 = Questions.GetQuestion(QuestionSet.Categories.AboutFBLA);

        // Use getters and setter to access and change the information
        Debug.Log(q1.Answer);
    }
}
