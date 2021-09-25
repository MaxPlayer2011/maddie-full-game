using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver02 : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        if (PlayerPrefs.GetString("GameOver") == "normal")
        {
            text.text = "DOORS95.APPLICATION.MADDIE.GAMEOVER.NPC.MAD_MAD";
        }

        else if (PlayerPrefs.GetString("GameOver") == "bus")
        {
            text.text = "DOORS95.APPLICATION.MADDIE.GAMEOVER.GAMEOBJECT.BUS";
        }

        else if (PlayerPrefs.GetString("GameOver") == "gorilla")
        {
            text.text = "DOORS95.APPLICATION.MADDIE.GAMEOVER.NPC.GORILLA";
        }

        else if (PlayerPrefs.GetString("GameOver") == "student_stampede")
        {
            text.text = "DOORS95.APPLICATION.MADDIE.GAMEOVER.NPC.ANNOYING.STUPID_STUDENT_STAMPEDE";
        }

        else if (PlayerPrefs.GetString("GameOver") == "camp")
        {
            text.text = "Nice job! You got " + PlayerPrefs.GetInt("campPoints") + " points!";
        }

        else if (PlayerPrefs.GetString("GameOver") == "plane")
        {
            text.text = "Nice job! You got " + PlayerPrefs.GetInt("planePoints") + " points!";
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}