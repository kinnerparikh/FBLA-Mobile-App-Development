using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public static int numCorrect = 0;
  

    // Start of program
    void Start()
    {
        // Get question based on the chosenTopic
        currentQNum++;
        SetQuestion(Questions.ReturnQuestion(Topic.chosenTopic));
    }

    // 
    public void Chosen1()
    {
        ChosenAnswer(1);
    }
    public void Chosen2()
    {
        ChosenAnswer(2);
    }

    public void Chosen3()
    {
        ChosenAnswer(3);
    }

    public void Chosen4()
    {
        ChosenAnswer(4);
    }

    private void ChosenAnswer(int a)
    {
        bool didLose = false;
        if (Int32.Parse(currentQuestion.Answer) != a)
        {
            GetButtonImage(a).color = Color.red; 
            Vibration.CreateOneShot(200);
            Debug.Log("Incorrect!! Correct choice is " + Int32.Parse(currentQuestion.Answer) + " you chose " + a);
            didLose = true;
            //Debug.Log("vibrate");
        }
        GetButtonImage(Int32.Parse(currentQuestion.Answer)).color = Color.green;
        if (Int32.Parse(currentQuestion.Answer) == a)
        {
            numCorrect++;
            Debug.Log("Correct!!");
        }

        choice1Text.GetComponentInParent<Button>().enabled = false;
        choice2Text.GetComponentInParent<Button>().enabled = false;
        choice3Text.GetComponentInParent<Button>().enabled = false;
        choice4Text.GetComponentInParent<Button>().enabled = false;

        StartCoroutine(ExecuteAfterTime(2, didLose));
        //SetQuestion(Questions.GetQuestion(Topic.chosenTopic));
    }

    //Get image of the button based on which question you selected (a = 1-4)
    private Image GetButtonImage(int a)
    {
        switch (a)
        {
            case 1:
                return choice1Text.GetComponentInParent<Image>();
            case 2:
                return choice2Text.GetComponentInParent<Image>();
            case 3:
                return choice3Text.GetComponentInParent<Image>();
            case 4:
                return choice4Text.GetComponentInParent<Image>();
            default:
                Debug.LogError("Question choice can only be 1-4");
                return null;
        }
    }


    IEnumerator ExecuteAfterTime(float time, bool didLose)
    {
        // 10 second delay
        yield return new WaitForSeconds(time);

        // if you get 10 questions right, turn to end screen
        if (numCorrect == 10 || didLose) 
            SceneManager.LoadScene("End");
        // else, continue to play
        else
            SceneManager.LoadScene("Topic");
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

        //Debug.Log("New Questions: " + q.Question);
    }



}
