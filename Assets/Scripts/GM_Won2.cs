using UnityEngine;
using UnityEngine.SceneManagement;

public class GM_Won2 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}