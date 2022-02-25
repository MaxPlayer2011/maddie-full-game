using UnityEngine;

public class AudioQueueManager : MonoBehaviour
{
	private int queueCount;
	private AudioSource audioSource;
	private AudioClip[] queueClips = new AudioClip[6466];
	private Subtitle subtitleManager;
	private string[] subtitleText = new string[6466];

	void Start()
	{
		audioSource = GetComponent<AudioSource>();

		if (GetComponent<Subtitle>() != null)
        {
			subtitleManager = GetComponent<Subtitle>();
		}
	}

	void Update()
	{
		if (!audioSource.isPlaying & queueCount > 0)
		{
			PlayQueue();
		}
	}

	public void Queue(AudioClip sound, string subtitleText = null)
	{
		queueClips[queueCount] = sound;
		this.subtitleText[queueCount] = subtitleText;
		queueCount++;
	}

	void PlayQueue()
	{
		audioSource.PlayOneShot(queueClips[0]);

		if (subtitleManager != null)
		{
			subtitleManager.text.text = subtitleText[0];
		}

		Unqueue();
	}

	void Unqueue()
	{
		for (int i = 0; i < queueCount; i++)
		{
			queueClips[i] = queueClips[i + 1];
			subtitleText[i] = subtitleText[i + 1];
		}

		queueCount--;
	}

	public void ClearQueue()
	{
		queueCount = 0;
	}
}