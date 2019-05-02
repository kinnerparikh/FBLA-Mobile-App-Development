using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Controls topic scene
public class TopicController : MonoBehaviour
{
    // All create GameObjects
    [SerializeField]
    private GameObject spinningWheel;
    [SerializeField]
    private GameObject spinButton;
    [SerializeField]
    private GameObject categoryText;

    [SerializeField]
    TMP_Text nameText;

    // Time interval for spinner
    [SerializeField]
    private float timeInterval = 0.01f;

    // Privates
    private int randomValue;
    private int finalAngle;

    // Constants
    [SerializeField]
    private int spinWheelMin = 600;
    [SerializeField]
    private int spinWheelMax = 1000;

    // Start to handle background music
    private void Start()
    {
        nameText.text = "Name: " + MenuController.username;
        FindObjectOfType<MusicManager>().Stop("ThemeSong");

        var gamePlayAudio = FindObjectOfType<MusicManager>().GetComponents<AudioSource>()[3];
        if (gamePlayAudio.clip != null && gamePlayAudio.time == 0) {
            FindObjectOfType<MusicManager>().Play("Gameplay");
        }
    }

    // To spin wheel
    public void Spin()
    {
        // Runs iEnumerator SpinWheel()
        StartCoroutine(SpinWheel());
    }

    // Spinning the wheel
    private IEnumerator SpinWheel()
    {

        float rotationalForce;
        randomValue = Random.Range(spinWheelMin, spinWheelMax);

        // Make spin button inactive
        spinButton.SetActive(false);

        // Change "category" text object to spinning
        categoryText.GetComponent<TextMeshProUGUI>().text = "Spinning...";

        // Actually rotating the wheel
        for (float i = 0; i < randomValue; i++)
        {
            // Random rotational force
            rotationalForce = Mathf.Min(5, randomValue / i * 0.3f);

            // Actually rotate the photo
            spinningWheel.transform.Rotate(0, 0, rotationalForce);

            // Stop spin
            if (i / randomValue > 0.18f)
                break;

            // Wait for 'timerInterval' seconds (pauses the loop)
            yield return new WaitForSeconds(timeInterval);
        }

        // If lands directly on middle of questions, rotate 2
        if (Mathf.RoundToInt(spinningWheel.transform.eulerAngles.z) % 72 == 0)
            transform.Rotate(0, 0, 2f);

        // Find the angle at which spin stops
        finalAngle = Mathf.RoundToInt((spinningWheel.transform.eulerAngles.z + 360 + 180) % 360);

        // Checks at which topic spinner lands on
        switch (Mathf.Floor((finalAngle) / 72))
        {
            // Case for "About FBLA" topic
            case 0:
                Topic.chosenTopic = QuestionSet.Categories.AboutFBLA;
                categoryText.GetComponent<TextMeshProUGUI>().text = "About FBLA";
                //Debug.Log("Choosed About FBLA");
                //Debug.Log(Topic.chosenTopic);
                break;

            // Case for "National Officers" topic
            case 1:
                Topic.chosenTopic = QuestionSet.Categories.NationalOfficers;
                categoryText.GetComponent<TextMeshProUGUI>().text = "National Officers";
                //Debug.Log("Choosed National Officers");
                //Debug.Log(Topic.chosenTopic);
                break;

            // Case for "FBLA History" topic
            case 2:
                Topic.chosenTopic = QuestionSet.Categories.FBLAHistory;
                categoryText.GetComponent<TextMeshProUGUI>().text = "FBLA History";
                //Debug.Log("Choosed FBLA History");
                //Debug.Log(Topic.chosenTopic);
                break;

            // Case for "Competitive Events" topic
            case 3:
                Topic.chosenTopic = QuestionSet.Categories.CompetitveEvents;
                categoryText.GetComponent<TextMeshProUGUI>().text = "Competitive Events";
                //Debug.Log("Choosed Competitive Events");
                //Debug.Log(Topic.chosenTopic);
                break;

            // Case for "Business Skills" topic
            case 4:
                Topic.chosenTopic = QuestionSet.Categories.BusinessSkills;
                categoryText.GetComponent<TextMeshProUGUI>().text = "Business Skills";
                //Debug.Log("Choosed Business Skills");
                //Debug.Log(Topic.chosenTopic);
                break;

            // Default
            default: break;
        }

        // Wait one second
        yield return new WaitForSeconds(1);

        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        // Load "Main" scene
        SceneManager.LoadScene("Main");

    }
}
