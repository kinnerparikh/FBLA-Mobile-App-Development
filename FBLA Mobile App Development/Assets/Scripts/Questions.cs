using System.Collections.Generic;
using UnityEngine;

/* ===========================
    * Questions Class
    * ===========================
    * Stores all questions in lists based on its categories for easy access by the user
    * ALWAYS run SetTotalQuestions at the start of the program to set the amount of questions in csv
    * ALWAYS run LoadAllQuestions to load the questions in to the lists (run this after SetTotalQuestions)
    * 
    */
// Stores all questions in lists based on its categories
public static class Questions
{
    // List of all question categories 
    public static List<QuestionSet> CompetitveEvents;
    public static List<QuestionSet> BusinessSkills;
    public static List<QuestionSet> AboutFBLA;
    public static List<QuestionSet> FBLAHistory;
    public static List<QuestionSet> NationalOfficers;

    // Number of questions answered so far 
    public static int QuestionCount = 0;

    // Questions that have been played already
    public static List<QuestionSet> questionsUsed = new List<QuestionSet>();

    // Return a new question from the category
    public static QuestionSet ReturnQuestion(QuestionSet.Categories cat)
    {

        while (true)
        {
            List<QuestionSet> qList;

            // Find the category list 
            if (cat == QuestionSet.Categories.AboutFBLA)
            {
                qList = AboutFBLA;
            }
            else if (cat == QuestionSet.Categories.BusinessSkills)
            {
                qList = BusinessSkills;
            }
            else if (cat == QuestionSet.Categories.CompetitveEvents)
            {
                qList = CompetitveEvents;
            }
            else if (cat == QuestionSet.Categories.FBLAHistory)
            {
                qList = FBLAHistory;
            }
            else
            {
                qList = NationalOfficers;
            }

            // Randomly select a question from the category
            System.Random rnd = new System.Random();
            int randomQ = rnd.Next(0, qList.Count - 1);

            QuestionSet q = qList[randomQ];

            // Add that question to questions 
            if (q.Used == false)
            {
                q.Used = true;
                questionsUsed.Add(q);
                return q;
            }
        }
    }

    // Load all questions from the database
    public static void LoadAllQuestions()
    {
        // Initialize lists for categories
        CompetitveEvents = new List<QuestionSet>();
        BusinessSkills = new List<QuestionSet>();
        AboutFBLA = new List<QuestionSet>();
        FBLAHistory = new List<QuestionSet>();
        NationalOfficers = new List<QuestionSet>();

        // Loop through all to load all the questions into the list
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

        //Debug.Log("Loaded Questions");
    }
}
