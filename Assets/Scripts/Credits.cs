using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        if (videoPlayer.isPlaying == false & videoPlayer.isPrepared == true)
        {
            SceneManager.LoadScene("MainMenu");
            PlayerPrefs.SetInt("won", 1);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}