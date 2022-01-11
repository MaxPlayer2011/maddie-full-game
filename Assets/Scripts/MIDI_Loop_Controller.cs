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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        switch (musicType)
        {
            case musicTypeType.Hell:
                midiLength = 3.875f;
                break;
            case musicTypeType.LearningHorror:
                midiLength = 4f;
                break;
        }

        timeToPlay = midiLength;
    }

    private void Update()
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
