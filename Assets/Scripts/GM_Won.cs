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
        timeToDie = Random.Range(10f, 15f);
    }

    void Update()
    {
        if (jumpscare)
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

            if (audioSource.volume == 0.5f)
            {
                audioSource.Stop();
                audioSource.volume = 1f;
            }

            audioSource.PlayOneShot(deathClip);
        }

        if (timeToKickstart < 0f)
        {
            SceneManager.LoadScene("Won2");
        }
    }
}
