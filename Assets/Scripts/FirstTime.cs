using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FirstTime : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    void Update()
    {
        if (dropdown.value == 0)
        {
            PlayerPrefs.SetString("lang", "eng");
        }

        else if (dropdown.value == 3)
        {
            PlayerPrefs.SetString("lang", "rus");
        }

        else if (dropdown.value == 2)
        {
            PlayerPrefs.SetString("lang", "pol");
        }

        else if (dropdown.value == 1)
        {
            PlayerPrefs.SetString("lang", "span");
        }
    }

    public void Confirm()
    {
        SceneManager.LoadScene("Warning");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerPrefs.SetInt("hasPlayedBefore", 1);
    }
}