using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    private float timeToQuit = 5f;
    public TextMeshProUGUI text;
    public Image image;
    public Sprite[] randomImages;

    void Start()
    {
        if (PlayerPrefs.GetString("GameOver") == "normal")
        {
            text.text = "Try better next time!";
        }

        else if (PlayerPrefs.GetString("GameOver") == "bus")
        {
            text.text = "Why did you get hit by that bus?!";
        }

        else if (PlayerPrefs.GetString("GameOver") == "gorilla")
        {
            text.text = "WHAT THE HELL?!?! A GORILLA?!?!";
        }

        else if (PlayerPrefs.GetString("GameOver") == "camp")
        {
            text.text = "Nice job! You got " + PlayerPrefs.GetInt("campPoints") + " points!";
        }

        else if (PlayerPrefs.GetString("GameOver") == "plane")
        {
            text.text = "Nice job! You got " + PlayerPrefs.GetInt("planePoints") + " points!";
        }

        image.sprite = randomImages[Random.Range(0, randomImages.Length)];
    }

    void Update()
    {
        timeToQuit -= Time.deltaTime;

        if (timeToQuit < 0f)
        {
            SceneManager.LoadScene("MainMenu");
            CustomAPI.CursorManager.Unlock();
        }
    }
}