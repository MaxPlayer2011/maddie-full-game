using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    private string begginingText = "DOORS.APPLICATION.MADDIE.GAMEOVER.";
    public TextMeshProUGUI text;

    private void Start()
    {
        switch (PlayerPrefs.GetString("GameOver"))
        {
            case "normal":
                text.text = begginingText + "NPC.MAD_MAD";
                break;
            case "bus":
                text.text = begginingText + "GAMEOBJECT.BUS";
                break;
            case "gorilla":
                text.text = begginingText + "NPC.GORILLA";
                break;
            case "stampede":
                text.text = begginingText + "NPC.ANNOYING.STUPID_STUDENT_STAMPEDE";
                break;
            case "camp":
                text.text = "Nice job! You got " + PlayerPrefs.GetInt("campPoints") + " points!";
                break;
            case "plane":
                text.text = "Nice job! You got " + PlayerPrefs.GetInt("planePoints") + " points!";
                break;
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu");
            GenericManagers.GUI.CursorManager.Unlock();
        }
    }
}
