using UnityEngine;
using UnityEngine.SceneManagement;

public class GM_Won : MonoBehaviour
{
    public float timeToDie;
    public float timeToKickstart;
    public bool jumpscare;
    private AudioSource audioSource;
    public AudioClip deathClip;
    public GameObject player;
    public GameObject maddie;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameObject.FindGameObjectWithTag("ACH_Canvas").GetComponent<AchievementManager>().CreateAchievement("BeatGame");
    }

    void Update()
    {
        if (jumpscare == true)
        {
            timeToKickstart -= Time.deltaTime * 1f;
        }

        else
        {
            timeToDie -= Time.deltaTime * 1f;
        }

        if (timeToDie < 0f)
        {
            jumpscare = true;
            player.SetActive(false);
            maddie.SetActive(true);
        }

        if (jumpscare == true)
        {
            audioSource.Stop();
            audioSource.volume = 0.3f;
            audioSource.PlayOneShot(deathClip, 7f);
        }
        
        if (timeToKickstart < 0f)
        {
            SceneManager.LoadScene("Won2");
        }
    }
}