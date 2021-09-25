using UnityEngine;

public class FAT_move : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource_loop;
    public AudioClip clip;

    void Start()
    {
        PlayAudio();
    }

    void Update()
    {
        
    }

    public void PlayAudio()
    {
        audioSource.Play();
        audioSource_loop.PlayDelayed(clip.length);
    }

    public void StopAudio()
    {
        audioSource.Stop();
        audioSource_loop.Stop();
    }
}