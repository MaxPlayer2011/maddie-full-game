using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    void Update()
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}