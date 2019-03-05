﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreText;

    public Button m_firstButton;

    void Start()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + GameController.numCorrect;
        m_firstButton.onClick.AddListener(ResetGame);
        FindObjectOfType<MusicManager>().Stop("Gameplay");
        FindObjectOfType<MusicManager>().Stop("ThemeSong");
        FindObjectOfType<MusicManager>().Play("ThemeSong");
    }

    // Resets game after a loss or after 10 questions answered correctly
    void ResetGame()
    {
        SceneManager.LoadScene("Start");
        GameController.numCorrect = 0;
        for (int i = 0; i < Questions.questionsUsed.Count; i++)
        {
            Questions.questionsUsed[i].Used = false;
        }
    }

    public void Share()
    {
        NativeShare ns = new NativeShare();
        ns.SetTitle("Check Out This AWESOME Game");
        ns.SetText("I Just GOT " + GameController.numCorrect + "/10 on the Fast Facts: FBLA QUIZ!! \n" +
            "https://github.com/kinzorPark/FBLA-Mobile-App-Development");
        ns.Share();
    }
}
