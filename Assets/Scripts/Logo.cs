using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    IEnumerator Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject.FindGameObjectWithTag("ACH_Canvas").GetComponent<AchievementManager>().CreateAchievement("StartGame");
        yield return new WaitForSeconds(5f);
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