using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NameEntryController : MonoBehaviour
{
    // 
    public TMP_InputField nameInput;


    public void InputName()
    {
        // Set the username in the input field to the username variable in MenuController
        MenuController.username = nameInput.GetComponent<TMP_InputField>().text;
        // Log Debugger
        Debug.Log(MenuController.username);
        // Load "start" scene
        SceneManager.LoadScene("Start");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
