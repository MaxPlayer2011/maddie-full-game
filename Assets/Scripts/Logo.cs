using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("Menu");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator Menu()
    {
        yield return new WaitForSeconds(5);

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