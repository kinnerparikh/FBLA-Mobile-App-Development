using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NameEntryController : MonoBehaviour
{

    public TMP_InputField nameInput;

    // 1. Attach to the NameEntry scene
    // 2. When the submit button is pressed, set field username to the text in
    // entry field
    public void InputName()
    {
        MenuController.username = nameInput.GetComponent<TMP_InputField>().text;
        if (MenuController.username != "")
        {
            SceneManager.LoadScene("Start");
        }
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
