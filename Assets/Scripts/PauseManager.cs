using UnityEngine;
using GenericManagers.GUI;

public class PauseManager : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (paused == true)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }

        if (paused == true & AudioListener.volume > 0f)
        {
            AudioListener.volume -= Time.unscaledDeltaTime * 3f;
        }

        if (paused == true & AudioListener.volume < 0f)
        {
            AudioListener.pause = true;
        }

        else
        {
            AudioListener.pause = false;
        }
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1f;
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        AudioListener.volume = 1f;
        CursorManager.Lock();
    }

    private void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        CursorManager.Unlock();
    }
}