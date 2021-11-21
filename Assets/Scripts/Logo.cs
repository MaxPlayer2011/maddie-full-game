using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomAPI;

public class Logo : MonoBehaviour
{
    IEnumerator Start()
    {
        if (!CursorManager.cursorLocked)
        {
            CursorManager.Lock();
        }

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Warning");
    }

    void Update()
    {
        if (Input.GetButtonDown("Skip Logo"))
        {
            SceneManager.LoadScene("Warning");
        }
    }
}