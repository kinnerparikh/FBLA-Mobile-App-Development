using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


/* ===========================
 * Question Database Class
 * ===========================
 * This static class loads all the questions from a csv files stored in format
 * 
 * Question, Choice1, Choice2, Choice3, Choice4, Answer, Category
 * 
 * Category are in integer format
 * CompetitveEvents = 1
 * BusinessSkills = 2
 * AboutFBLA = 3
 * FBLAHistory = 4
 * NationalOfficers = 5
 * 
 * IMPORTANT: At the start of program, run import game function with parameter of CSV file path
 * 
 */
public static class QuestionDatabase
{
    // Stores all questions, choices, answer, and type of every questions
    public static List<string> questions;
    public static List<string> choice1s;
    public static List<string> choice2s;
    public static List<string> choice3s;
    public static List<string> choice4s;
    public static List<string> answers;
    public static List<string> categories;

    // Load Questions from a CSV file for easy access
    public static void ImportGame(string path)
    {
        using (var reader = new StreamReader(@path))
        {
            
            questions = new List<string>();
            choice1s = new List<string>();
            choice2s = new List<string>();
            choice3s = new List<string>();
            choice4s = new List<string>();
            answers = new List<string>();
            categories = new List<string>();
            

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                questions.Add(values[0].ToString());
                choice1s.Add(values[1].ToString());
                choice2s.Add(values[2].ToString());
                choice3s.Add(values[3].ToString());
                choice4s.Add(values[4].ToString());
                answers.Add(values[5].ToString());
                categories.Add(values[6].ToString());
            }
        }
    }

}
