using System.Collections;
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
    }

    // Resets game after a loss or after 10 questions answered correctly
    void ResetGame()
    {
        SceneManager.LoadScene("Topic");
        GameController.numCorrect = 0;
        for (int i = 0; i < Questions.questionsUsed.Count; i++)
        {
            Questions.questionsUsed[i].Used = false;
        }
    }
}
