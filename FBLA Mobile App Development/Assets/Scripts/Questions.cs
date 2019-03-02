using System.Collections;
using System.Collections.Generic;
using System;

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
    // Change depend on the amount of questions, used for RNG to generate questions
    public static int totalQuestions;

    // Stores all questions in lists based on its categories
    public static List<QuestionSet> CompetitveEvents;
    public static List<QuestionSet> BusinessSkills;
    public static List<QuestionSet> AboutFBLA;
    public static List<QuestionSet> FBLAHistory;
    public static List<QuestionSet> NationalOfficers;

    public static QuestionSet GetQuestion(QuestionSet.Categories cat)
    {
        while (true)
        {
            Random rnd = new Random();
            int randomQ = rnd.Next(0, totalQuestions);

            QuestionSet q = new QuestionSet(randomQ);
            if (q.Category == cat)
            {
                return q;
            }
            else
            {
                q = null;
            }
        }
    }

    public static void SetTotalQuestions(int tq)
    {
        totalQuestions = tq;
    }

    public static void LoadAllQuestions()
    {
        CompetitveEvents = new List<QuestionSet>();
        BusinessSkills = new List<QuestionSet>();
        AboutFBLA = new List<QuestionSet>();
        FBLAHistory = new List<QuestionSet>();
        NationalOfficers = new List<QuestionSet>();

        // Loop Through All to load all the questions into the list
        for (int i = 0; i < totalQuestions; i++)
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
    }
}
