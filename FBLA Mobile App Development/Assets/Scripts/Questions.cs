using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class Questions
{
    /* ===========================
    * Questions Class
    * ===========================
    * Stores all questions in lists based on its categories for easy access by the user
    * ALWAYS run SetTotalQuestions at the start of the program to set the amount of questions in csv
    * ALWAYS run LoadAllQuestions to load the questions in to the lists (run this after SetTotalQuestions)
    * 
    */
    // Stores all questions in lists based on its categories
    public static List<QuestionSet> CompetitveEvents;
    public static List<QuestionSet> BusinessSkills;
    public static List<QuestionSet> AboutFBLA;
    public static List<QuestionSet> FBLAHistory;
    public static List<QuestionSet> NationalOfficers;

    public static int QuestionCount = 0;
    //public static List<QuestionSet> questionsUsed = new List<QuestionSet>();

    public static QuestionSet ReturnQuestion(QuestionSet.Categories cat)
    {
        //bool used = false;

        while (true)
        {
            //used = false; 
            System.Random rnd = new System.Random();
            int randomQ = rnd.Next(0, QuestionDatabase.questions.Count);

            QuestionSet q = new QuestionSet(randomQ);
            
            /*for (int i = 0; i < questionsUsed.Count; i++)
            {
                if (questionsUsed[i].Equals(q))
                {
                    q = null;
                    used = true;
                }
            }*/

            //if (!used)
            //{
                if (q.Category == cat)
                {
                    return q;
                    //questionsUsed.Add(q);
                }
                else
                {
                    q = null;
                }
            //}
        }
    }
    

    public static void LoadAllQuestions()
    {

        CompetitveEvents = new List<QuestionSet>();
        BusinessSkills = new List<QuestionSet>();
        AboutFBLA = new List<QuestionSet>();
        FBLAHistory = new List<QuestionSet>();
        NationalOfficers = new List<QuestionSet>();

        // Loop Through All to load all the questions into the list
        for (int i = 0; i < QuestionDatabase.questions.Count; i++)
        {
            QuestionSet q = new QuestionSet(i);

            if (q.Category == QuestionSet.Categories.CompetitveEvents)
            {
                CompetitveEvents.Add(q);
            }
            else if (q.Category == QuestionSet.Categories.BusinessSkills)
            {
                BusinessSkills.Add(q);
            }
            else if (q.Category == QuestionSet.Categories.AboutFBLA)
            {
                AboutFBLA.Add(q);
            }
            else if (q.Category == QuestionSet.Categories.FBLAHistory)
            {
                FBLAHistory.Add(q);
            }
            else
            {
                NationalOfficers.Add(q);
            }
        }

        Debug.Log("Loaded Questions");
    }
}
