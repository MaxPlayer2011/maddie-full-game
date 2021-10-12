using UnityEngine;

public class MIDI_Loop_Controller : MonoBehaviour
{
    public bool unscaled;
    private float timeToPlay;
    private float midiLength;
    private AudioSource audioSource;
    public enum musicTypeType
    {
        Null,
        LearningHorror,
        Hell
    }
    public musicTypeType musicType;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (musicType == musicTypeType.Hell)
        {
            midiLength = 3.875f;
        }

        else if (musicType == musicTypeType.LearningHorror)
        {
            midiLength = 4f;
        }

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