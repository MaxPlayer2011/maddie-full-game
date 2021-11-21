using UnityEngine;
using UnityEngine.SceneManagement;
using CustomAPI;

public class GM_Won2 : MonoBehaviour
{
    void Update()
    {
        if (!CursorManager.cursorLocked)
        {
            CursorManager.Lock();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Credits");
        }
    }
}