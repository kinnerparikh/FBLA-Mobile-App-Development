using System;
using System.Collections.Generic;
using System.IO;

// Class that stores name and score
public class Score
{
    public String Name { get; set; }
    public int Val { get; set; }
}

// Manages list of high scores
public class HighScores
{
    // Current list of high scores
    static List<Score> m_currentList;

    // Remembers if list of scores read from file 
    static bool m_fFileRead = false;

    // Ensures scores from file have been read exactly once
    private static void EnsureFileRead()
    {
        // If list of scores NOT read from file
        if (!m_fFileRead)
        {
            // Instantiate list of high scores
            m_currentList = new List<Score>();

            // Checking if file named "hs" exists
            if (File.Exists("hs"))
            {
                // Open file as text so can read file
                using (StreamReader sr = File.OpenText("hs"))
                {
                    String line;

                    // Read line by line until end of file
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Parse line into 1.Name and 2.Score based on ','
                        var sa = line.Split(',');

                        // Create a score obj and add it to list
                        m_currentList.Add(new Score() { Name = sa[0], Val = int.Parse(sa[1]) });
                    }
                }
            }

            // Set fileRead to true
            m_fFileRead = true;
        }
    }

    // Saves scores to "hs" file
    public void SaveScores()
    {
        // Open file as text so can write on file
        using (StreamWriter sw = File.CreateText("hs"))
        {
            // Loop through all elements of currentList
            foreach(Score sc in m_currentList)
            {
                // Write comma separated 1.Name and 2.Score into file "hs"
                sw.WriteLine(sc.Name + ',' + sc.Val.ToString());
            }
        }
    }

    // Set highScore based on Name
    public static  void AddHighScore(String name, int score)
    {
        // Makes sure file has been read
        EnsureFileRead();

        // Find out if the player exists by looping through m_currentList
        foreach (Score sc in m_currentList)
        {
            // If name found in m_currentList equals player
            if (sc.Name == name)
            {
                // If the new score is higher than old
                if (sc.Val < score)
                    // Set score to new HIGHER score
                    sc.Val = score;
                    
                return;
            }
        }

        // When player not exist in currentList, create new player and add 1.Name and 2.Val to list
        m_currentList.Add(new Score() { Name = name, Val = score });
    }

    // Gets list of name and scores, sorted by ascending order of score
    public static List<Score> GetScores()
    {
        // Makes sure file has been read
        EnsureFileRead();

        // Sort m_currentList
        m_currentList.Sort(

            // Delegate is a funtion passed as a parameter to the sort function
            delegate (Score s1, Score s2)
            {
                // If scores are equal, do nothing
                if (s1.Val == s2.Val) return 0;
                
                // If score1 is less than score2, put score2 above score1
                else if (s1.Val < s2.Val) return -1;

                // If score1 is greater than score2, put score1 above score2
                else return 1;
            }
        );

        // return the sorted list
        return m_currentList;
    }
}

