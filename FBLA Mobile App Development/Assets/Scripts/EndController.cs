using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndController : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreText;


    void Start()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + GameController.numCorrect;
    }

}
