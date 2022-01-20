using System.Collections;
using UnityEngine;
using GenericManagers;

public class StudentStampede : MonoBehaviour
{
    private int previousTexture;
    private bool killingPlayer;
    private AudioSource audioSource;
    public SpriteRenderer[] sprites;
    public Sprite[] spriteTextures;
    public Transform spriteParent;
    
    private IEnumerator Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        for (int i = 0; i < spriteParent.childCount; i++)
        {
            sprites[i] = spriteParent.GetChild(i).GetComponent<SpriteRenderer>();
        }

        if (sprites[sprites.Length - 1] != null)
        {
            for (int i = sprites.Length - 1; i > 0; i--)
            {
                int num = Random.Range(0, spriteTextures.Length);

                while (previousTexture == num)
                    num = Random.Range(0, spriteTextures.Length);

                sprites[i].sprite = spriteTextures[num];
                previousTexture = num;
            }
        }

        yield return new WaitForSeconds(30f);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (killingPlayer)
        {
            transform.position += transform.forward * 20f * Time.unscaledDeltaTime;
        }

        else
        {
            transform.position += transform.forward * 20f * Time.deltaTime;
        }

        if (audioSource.volume < 1f)
        {
            audioSource.volume += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.squished = true;
            player.stampedeSquishPos = new Vector3(transform.position.x, 0, transform.position.z);
            killingPlayer = true;
            StartCoroutine(KillPlayer());
        }
    }

    private IEnumerator KillPlayer()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(5f);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GameOverEnd("stampede");
    }
}
