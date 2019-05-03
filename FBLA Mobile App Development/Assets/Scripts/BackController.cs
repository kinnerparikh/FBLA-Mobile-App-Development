using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackController : MonoBehaviour
{
    //Called when back button is clicked
    public void Back()
    {
        // Go to "Start" scene
        SceneManager.LoadScene("Start");
    }
}
