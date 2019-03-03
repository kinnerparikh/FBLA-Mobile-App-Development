﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    /* ===========================
    * Game Controller Class
    * ===========================
    * 
    * Controller class controls how the game is ran in unity and sets everything up
    * 
    */

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
        QuestionDatabase.ImportGame("Assets/Test.csv");
        // Load all the strings into lists of Question Set Objects organized by categories
        Questions.LoadAllQuestions();

        // Get first question based on the choosenTopic
        currentQNum++;
        SetQuestion(Questions.GetQuestion(Topic.choosenTopic));
    }

    public void Choosed1()
    {
        if (Int32.Parse(currentQuestion.Answer) == 1)
        {
            numCorrect++;
            Debug.Log("Correct!!");
        }
        else
        {
            Debug.Log("Incorrect!! Correct choice is " + Int32.Parse(currentQuestion.Answer) + " you chose 1");
        }
        SetQuestion(Questions.GetQuestion(Topic.choosenTopic));
    }
    public void Choosed2()
    {
        if (Int32.Parse(currentQuestion.Answer) == 2)
        {
            numCorrect++;
            Debug.Log("Correct!!");
        }
        else
        {
            Debug.Log("Incorrect!! Correct choice is " + Int32.Parse(currentQuestion.Answer) + " you chose 2");
        }
        SetQuestion(Questions.GetQuestion(Topic.choosenTopic));
    }

    public void Choosed3()
    {
        if (Int32.Parse(currentQuestion.Answer) == 3)
        {
            numCorrect++;
            Debug.Log("Correct!!");
        }
        else
        {
            Debug.Log("Incorrect!! Correct choice is " + Int32.Parse(currentQuestion.Answer) + " you chose 3");
        }
        SetQuestion(Questions.GetQuestion(Topic.choosenTopic));
    }

    public void Choosed4()
    {
        if (Int32.Parse(currentQuestion.Answer) == 4)
        {
            numCorrect++;
            Debug.Log("Correct!!");
        }
        else
        {
            Debug.Log("Incorrect!! Correct choice is " + Int32.Parse(currentQuestion.Answer) + " you chose 4");
        }
        SetQuestion(Questions.GetQuestion(Topic.choosenTopic));
    }


    // Update is called once per frame
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

        Debug.Log("New Questions: " + q.Question);
    }



}
