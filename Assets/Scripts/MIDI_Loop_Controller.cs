using UnityEngine;

public class MIDI_Loop_Controller : MonoBehaviour
{
    public bool unscaled;
    private float timeToPlay;
    public float midiLength;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeToPlay = midiLength;
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            if (!unscaled)
            {
                timeToPlay -= Time.deltaTime * audioSource.pitch;
            }

            else
            {
                timeToPlay -= Time.unscaledDeltaTime * audioSource.pitch;
            }

            if (timeToPlay < 0f)
            {
                timeToPlay = midiLength;
                audioSource.PlayOneShot(audioSource.clip);
            }
        }
    }
}