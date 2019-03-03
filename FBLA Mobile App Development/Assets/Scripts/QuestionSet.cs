using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class QuestionSet
{

    /* ===========================
    * Question Set Class
    * ===========================
    * 
    * Data structure for storing a set of questions
    * Contains the questions, choices, and answer in string
    * Also contains the type of question in an enum (Categories)
    * 
    */
    private string question;
    private string choice1;
    private string choice2;
    private string choice3;
    private string choice4;
    private string answer;

    private Categories category;

    // Enum for types of categories
    public enum Categories
    {
        CompetitveEvents, //1
        BusinessSkills, //2 
        AboutFBLA, //3
        FBLAHistory, //4
        NationalOfficers //5
    }

    // Constructor for creating a questions
    // No need for user to call this function, used by the static Questions Class
    public QuestionSet (int question)
    {
        // Load Question set
        this.Question = load(question, 0);
        this.Choice1 = load(question, 1);
        this.Choice2 = load(question, 2);
        this.Choice3 = load(question, 3);
        this.Choice4 = load(question, 4);
        this.Answer = load(question, 5);
        String stringCat = load(question, 6);

        // Set category from integer
        switch (Convert.ToInt32(stringCat))
        {
            case 1:
                this.category = Categories.CompetitveEvents;
                break;
            case 2:
                this.category = Categories.BusinessSkills;
                break;
            case 3:
                this.category = Categories.AboutFBLA;
                break;
            case 4:
                this.category = Categories.FBLAHistory;
                break;
            case 5:
                this.category = Categories.NationalOfficers;
                break;
            default:
                // Defualt as Competitive Events
                Debug.LogError("ERROR: Categories can only be from 1 - 5");

                this.category = Categories.CompetitveEvents;
                break;
        }
    }
    

    // Used to return string from the lists in the Question Database
    private string load(int question, int type)
    {

        List<string> list = new List<string>();
        switch (type)
        {
            case 0:
                list = QuestionDatabase.questions;
                break;
            case 1:
                list = QuestionDatabase.choice1s;
                break;
            case 2:
                list = QuestionDatabase.choice2s;
                break;
            case 3:
                list = QuestionDatabase.choice3s;
                break;
            case 4:
                list = QuestionDatabase.choice4s;
                break;
            case 5:
                list = QuestionDatabase.answers;
                break;
            case 6:
                list = QuestionDatabase.categories;
                break;
            default:
                // Default get the question string
                Debug.LogError("ERROR: Type Can Only Be From 0 - 5");

                list = QuestionDatabase.questions;
                break;
        }

        Debug.Log(list);
        return list[question];
    }

    // Getters and Setters
    public string Question
    {
        get { return question; }
        set { question = value; }
    }   
    public string Choice1
    {
        get { return choice1; }
        set { choice1 = value; }
    }
    public string Choice2
    {
        get { return choice2; }
        set { choice2 = value; }
    }
    public string Choice3
    {
        get { return choice3; }
        set { choice3 = value; }
    }
    public string Choice4
    {
        get { return choice4; }
        set { choice4 = value; }
    }
    public string Answer
    {
        get { return answer; }
        set { answer = value; }
    }

    public Categories Category
    {
        get { return category; }
        set { category = value; }
    }



}
