using UnityEngine;
using UnityEngine.SceneManagement;

public class GM_Kickstart : MonoBehaviour
{
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            Application.OpenURL("");
        }*/

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}