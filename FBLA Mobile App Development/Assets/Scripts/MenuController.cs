using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    /* ===========================
    * Question Database Class
    * ===========================
    * This class controls the start scene
    */
    
    // Called when Start Button is Pressed
    public void StartGame()
    {
        // Load the Scene to choose topic
        SceneManager.LoadScene("Topic");
    }
}
