using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    private FPSCounter fps;
    public GameObject ohno;

    void Start()
    {
        fps = GetComponent<FPSCounter>();

        if (PlayerPrefs.GetInt("currentFloor") == 0)
        {
            PlayerPrefs.SetInt("currentFloor", 1);
        }
    }

    void Update()
    {
        debugText.text = "DEBUG\nVersion: " + Application.version + ", Unity Version: " + Application.unityVersion + ",\nFPS: " + fps.m_CurrentFps + ",\ncurrentFloor PlayerPrefs: " + PlayerPrefs.GetInt("currentFloor");
    }

    public void OpenFloor(int floor)
    {
        if (floor <= PlayerPrefs.GetInt("currentFloor"))
        {
            if (floor == 1)
            {
                SceneManager.LoadScene("MainLevel");
            }

            else if (floor == 2)
            {
                SceneManager.LoadScene("Floor2");
            }
        }

        else
        {
            ohno.SetActive(true);
        }
    }
}