using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class GameControllerOld : MonoBehaviour
{
    /* ===========================
    * Game Controller Class
    * ===========================
    * 
    * Controller class controls how the game is ran in unity and sets everything up
    * 
    */

    // Game Objects : Question
    [SerializeField]
    private GameObject questionsText;

    // Game Objects : Choices
    [SerializeField]
    private GameObject choice1Text;
    [SerializeField]
    private GameObject choice2Text;
    [SerializeField]
    private GameObject choice3Text;
    [SerializeField]
    private GameObject choice4Text;

    // Current Question
    private int currentQNum = 0;
    private QuestionSet currentQuestion;

    // Stats
    private int numCorrect = 0;
  

    // Start of program
    void Start()
    {
        // Always run the following in order

        // Imports questions to lists of strings from csv
        QuestionDatabase.ImportGame("Test");
        // Load all the strings into lists of Question Set Objects organized by categories
        Questions.LoadAllQuestions();

        // Get first question based on the chosenTopic
        currentQNum++;
        SetQuestion(Questions.ReturnQuestion(Topic.chosenTopic));
    }

    // 
    public void Chosen1()
    {
        ChosenAnswer(1);
        Debug.Log("ChosenAnswer(1)");
    }
    public void Chosen2()
    {
        ChosenAnswer(2);
        Debug.Log("ChosenAnswer(2)");
    }

    public void Chosen3()
    {
        ChosenAnswer(3);
        Debug.Log("ChosenAnswer(3)");
    }

    public void Chosen4()
    {
        ChosenAnswer(4);
        Debug.Log("ChosenAnswer(4)");
    }

    private void ChosenAnswer(int a)
    {
        if (Int32.Parse(currentQuestion.Answer) == a)
        {
            numCorrect++;
            Debug.Log("Correct!!");
        }
        else
        {
            Vibration.CreateOneShot(200);
            Debug.Log("Incorrect!! Correct choice is " + Int32.Parse(currentQuestion.Answer) + " you chose " + a);
            //Debug.Log("vibrate");
        }
        SetQuestion(Questions.ReturnQuestion(Topic.chosenTopic));
    }


    // Update is called ze per frame
    void Update()
    {
        // TO USE ====================
        // Create new question set and call function Questions.GetQuestion(Category) to get a random question from that category
        // === QuestionSet q1 = Questions.GetQuestion(QuestionSet.Categories.AboutFBLA);

        // Use getters and setter to access and change the information
        // === Debug.Log(q1.Answer); 

        /* Testing Code
        if (Input.GetKeyDown(KeyCode.Space))
        {
            newQuestion();
        }
        */
    }


    // Set Question onto the UI
    private void SetQuestion(QuestionSet q)
    {
   
        questionsText.GetComponent<TextMeshProUGUI>().text = q.Question;
        choice1Text.GetComponent<TextMeshProUGUI>().text = q.Choice1;
        choice2Text.GetComponent<TextMeshProUGUI>().text = q.Choice2;
        choice3Text.GetComponent<TextMeshProUGUI>().text = q.Choice3;
        choice4Text.GetComponent<TextMeshProUGUI>().text = q.Choice4;
        currentQuestion = q;

        //Debug.Log("New Questions: " + q.Question);
    }



}
