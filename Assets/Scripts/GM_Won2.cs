using UnityEngine;
using UnityEngine.SceneManagement;

public class GM_Won2 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Credits");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}