using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField]
    private GameObject questionsText;

    [SerializeField]
    private GameObject choice1Text;
    [SerializeField]
    private GameObject choice2Text;
    [SerializeField]
    private GameObject choice3Text;
    [SerializeField]
    private GameObject choice4Text;



    // Start of program
    void Start()
    {
        // Always run the following in order

        // Imports questions to lists of strings from csv
        QuestionDatabase.ImportGame("Assets/Test.csv");
        // Set the total amount of questions for the RNG
        Questions.SetTotalQuestions(totalQuestions);
        // Load all the strings into lists of Question Set Objects organized by categories
        Questions.LoadAllQuestions();
    }

    // Update is called once per frame
    void Update()
    {
        // TO USE ====================
        // Create new question set and call function Questions.GetQuestion(Category) to get a random question from that category
        // === QuestionSet q1 = Questions.GetQuestion(QuestionSet.Categories.AboutFBLA);

        // Use getters and setter to access and change the information
        // === Debug.Log(q1.Answer);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            newQuestion();
        }
    }

    private void newQuestion()
    {
        QuestionSet q1 = Questions.GetQuestion(QuestionSet.Categories.CompetitveEvents);
        questionsText.GetComponent<TextMeshProUGUI>().text = q1.Question;
        choice1Text.GetComponent<TextMeshProUGUI>().text = q1.Choice1;
        choice2Text.GetComponent<TextMeshProUGUI>().text = q1.Choice2;
        choice3Text.GetComponent<TextMeshProUGUI>().text = q1.Choice3;
        choice4Text.GetComponent<TextMeshProUGUI>().text = q1.Choice4;

        Debug.Log("New Questions: " + q1.Question);
    }
}
