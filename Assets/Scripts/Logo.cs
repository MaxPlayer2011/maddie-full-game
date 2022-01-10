using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GenericManagers.GUI;

public class Logo : MonoBehaviour
{
    private IEnumerator Start()
    {
        if (!CursorManager.cursorLocked)
        {
            CursorManager.Lock();
        }

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Warning");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Skip Logo"))
        {
            SceneManager.LoadScene("Warning");
        }
    }
}
