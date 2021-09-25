using UnityEngine;

public class Lang_to_Lang_AUDIO : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip_eng;
    public AudioClip clip_rus;
    public AudioClip clip_span;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetString("lang") == "eng")
        {
            audioSource.clip = clip_eng;
        }

        else if (PlayerPrefs.GetString("lang") == "rus")
        {
            audioSource.clip = clip_rus;
        }

        else if (PlayerPrefs.GetString("lang") == "span")
        {
            audioSource.clip = clip_span;
        }
    }
}