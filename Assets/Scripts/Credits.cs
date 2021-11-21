using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomAPI;

public class Credits : MonoBehaviour
{
    public Animator anim;
    public GameObject mus;

    IEnumerator Start()
    {
        if (!CursorManager.cursorLocked)
        {
            CursorManager.Lock();
        }

        PlayerPrefs.SetInt("won", 1);
        DontDestroyOnLoad(mus);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene("MainMenu");
        CursorManager.Unlock();
    }
}