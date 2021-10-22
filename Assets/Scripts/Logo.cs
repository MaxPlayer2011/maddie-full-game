using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    IEnumerator Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(5f);
        LoadGame();
    }

    void Update()
    {
        if (Input.GetButtonDown("Skip Logo"))
        {
            LoadGame();
        }
    }

    void LoadGame()
    {
        if (PlayerPrefs.GetInt("hasPlayedBefore") == 0 & PlayerPrefs.GetInt("debug") == 1)
        {
            SceneManager.LoadScene("FirstTime");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            SceneManager.LoadScene("Warning");
        }
    }
}