using UnityEngine;
using UnityEngine.SceneManagement;

public class Warning : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu");
            GenericManagers.GUI.CursorManager.Unlock();
        }
    }
}