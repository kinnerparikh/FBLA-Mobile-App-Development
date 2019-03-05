using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TopicController : MonoBehaviour
{
    [SerializeField]
    private GameObject spinningWheel;
    [SerializeField]
    private GameObject spinButton;
    [SerializeField]
    private float timeInterval = 0.01f;

    [SerializeField]
    private GameObject categoryText;

    private int randomValue;
    private int finalAngle;

    [SerializeField]
    private int spinWheelMin = 600;
    [SerializeField]
    private int spinWheelMax = 1000;

    private void Start()
    {
        FindObjectOfType<MusicManager>().Stop("Gameplay");
        FindObjectOfType<MusicManager>().Stop("ThemeSong");
        FindObjectOfType<MusicManager>().Play("Gameplay");
    }

    public void Spin()
    {
        StartCoroutine(SpinWheel());
    }

    private IEnumerator SpinWheel()
    {
        float rotationalForce;
        randomValue = Random.Range(spinWheelMin, spinWheelMax);

        spinButton.SetActive(false);

        categoryText.GetComponent<TextMeshProUGUI>().text = "Spinning...";

        for (float i = 0; i < randomValue; i++)
        {
            rotationalForce = Mathf.Min(5, randomValue / i * 0.3f);
            spinningWheel.transform.Rotate(0, 0, rotationalForce);
            if (i / randomValue > 0.18f)
                break;
            
            yield return new WaitForSeconds(timeInterval);
        }

        if (Mathf.RoundToInt(spinningWheel.transform.eulerAngles.z) % 72 == 0)
            transform.Rotate(0, 0, 2f);

        finalAngle = Mathf.RoundToInt((spinningWheel.transform.eulerAngles.z + 360 + 180) % 360);

        switch (Mathf.Floor((finalAngle) / 72))
        {
            case 0:
                Topic.chosenTopic = QuestionSet.Categories.AboutFBLA;
                categoryText.GetComponent<TextMeshProUGUI>().text = "About FBLA";
                Debug.Log("Choosed About FBLA");
                Debug.Log(Topic.chosenTopic);
                break;
            case 1:
                Topic.chosenTopic = QuestionSet.Categories.NationalOfficers;
                categoryText.GetComponent<TextMeshProUGUI>().text = "National Officers";
                Debug.Log("Choosed National Officers");
                Debug.Log(Topic.chosenTopic);
                break;
            case 2:
                Topic.chosenTopic = QuestionSet.Categories.FBLAHistory;
                categoryText.GetComponent<TextMeshProUGUI>().text = "FBLA History";
                Debug.Log("Choosed FBLA History");
                Debug.Log(Topic.chosenTopic);
                break;
            case 3:
                Topic.chosenTopic = QuestionSet.Categories.CompetitveEvents;
                categoryText.GetComponent<TextMeshProUGUI>().text = "Competitive Events";
                Debug.Log("Choosed Competitive Events");
                Debug.Log(Topic.chosenTopic);
                break;
            case 4:
                Topic.chosenTopic = QuestionSet.Categories.BusinessSkills;
                categoryText.GetComponent<TextMeshProUGUI>().text = "Business Skills";
                Debug.Log("Choosed Business Skills");
                Debug.Log(Topic.chosenTopic);
                break;
            default: break;
        }

        yield return new WaitForSeconds(1);

        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene("Main");

    }
}
