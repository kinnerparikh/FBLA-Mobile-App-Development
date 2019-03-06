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
    [SerializeField]
    private GameObject timeText;

    // Current Question
    private int currentQNum = 0;
    private QuestionSet currentQuestion;

    // Stats
    public static int numCorrect = 0;

    // How much time is left in the timer
    float currCountdownValue;
  

    // Start of program
    void Start()
    {
        // Get question based on the chosenTopic
        currentQNum++;
        SetQuestion(Questions.ReturnQuestion(Topic.chosenTopic));
    }

    // Called when 1st button is clicked
    public void Chosen1()
    {
        ChosenAnswer(1);
    }

    // Called when 2nd button is clicked
    public void Chosen2()
    {
        ChosenAnswer(2);
    }

    // Called when 3rd button is clicked
    public void Chosen3()
    {
        ChosenAnswer(3);
    }

    // Called when 4th button is clicked
    public void Chosen4()
    {
        ChosenAnswer(4);
    }

    //Logic for when an answer is chosen
    private void ChosenAnswer(int a)
    {
        //Did you select the correct answer?
        bool didLose = false;

        //if answer is wrong
        if (Int32.Parse(currentQuestion.Answer) != a)
        {
            //
            GetButtonImage(a).color = Color.red; 
            Vibration.CreateOneShot(200);
            //Debug.Log("Incorrect!! Correct choice is " + Int32.Parse(currentQuestion.Answer) + " you chose " + a);
            didLose = true;
            FindObjectOfType<MusicManager>().Play("Incorrect");
            //Debug.Log("vibrate");
        }

        // Set correct answer button to green
        GetButtonImage(Int32.Parse(currentQuestion.Answer)).color = Color.green;

        // If answer is correct 
        if (Int32.Parse(currentQuestion.Answer) == a)
        {
            numCorrect++;
            Debug.Log("Correct!!");
            FindObjectOfType<MusicManager>().Play("Correct");
        }

        // Disable all buttons
        choice1Text.GetComponentInParent<Button>().enabled = false;
        choice2Text.GetComponentInParent<Button>().enabled = false;
        choice3Text.GetComponentInParent<Button>().enabled = false;
        choice4Text.GetComponentInParent<Button>().enabled = false;

        StartCoroutine(LoadEnd(2, didLose));
        //SetQuestion(Questions.GetQuestion(Topic.chosenTopic));
    }

    // Get image of the button based on which question you selected (a = 1-4)
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

    // Loads appropriate scene based on "didLose" and/or "score" 
    IEnumerator LoadEnd(float time, bool didLose)
    {
        // 10 second delay
        Debug.Log("Timer Started");
        yield return new WaitForSeconds(time);
        Debug.Log("Timer Finished");

        // if you get 10 questions right, turn to end screen
        if (numCorrect == 10 || didLose) 
            SceneManager.LoadScene("End");
        // else, continue to play
        else
            SceneManager.LoadScene("Topic");
    }

    // Start 15 second countdown
    public IEnumerator StartCountdown(float countdownValue = 15)
    {
        // Update the current countdown value
        currCountdownValue = countdownValue;

        // While timer is not done
        while (currCountdownValue > 0)
        {
            //Debug.Log("Countdown: " + currCountdownValue);

            // Wait 1 second
            yield return new WaitForSeconds(1.0f);

            // Decrease current countdown value and update time text in UI 
            timeText.GetComponent<TextMeshProUGUI>().text = "Time: " + countdownValue;
            currCountdownValue--;
        }
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
