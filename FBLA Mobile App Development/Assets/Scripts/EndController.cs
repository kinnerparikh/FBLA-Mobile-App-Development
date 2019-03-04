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
        m_firstButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("Topic");
        GameController.numCorrect = 0;
    }
}
