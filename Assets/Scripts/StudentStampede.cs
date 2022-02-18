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
                int[] sprite1Locations = new int[2]
                {
                    3,
                    -1
                };
                int[] sprite2Locations = new int[2]
                {
                    1,
                    -4
                };
                int[] sprite3Locations = new int[2]
                {
                    2,
                    -1
                };
                int[] sprite4Locations = new int[2]
                {
                    1,
                    -2
                };
                int[] sprite5Locations = new int[2]
                {
                    0,
                    -2
                };
                Vector3 spritePos = new Vector3();
                float spriteScale = 0;

                while (previousTexture == num)
                    num = Random.Range(0, spriteTextures.Length);

                switch (num)
                {
                    case 0:
                        spritePos = ToSpritePosition(sprite1Locations);
                        spriteScale = 12.5f;
                        break;
                    case 1:
                        spritePos = ToSpritePosition(sprite2Locations);
                        spriteScale = 15f;
                        break;
                    case 2:
                        spritePos = ToSpritePosition(sprite3Locations);
                        spriteScale = 10f;
                        break;
                    case 3:
                        spritePos = ToSpritePosition(sprite4Locations);
                        spriteScale = 10f;
                        break;
                    case 4:
                        spritePos = ToSpritePosition(sprite5Locations);
                        spriteScale = 11f;
                        break;
                }

                sprites[i].transform.localPosition = new Vector3(spritePos.x, spritePos.y, sprites[i].transform.localPosition.z);
                sprites[i].transform.localScale = new Vector3(spriteScale, spriteScale);
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

    private static Vector3 ToSpritePosition(int[] nums)
    {
        Vector3[] positions = new Vector3[4]
        {
            new Vector3(nums[0], nums[1]),
            new Vector3(-nums[0], nums[1]),
            new Vector3(nums[0], -5f),
            new Vector3(-nums[0], -5f)
        };

        return positions[Random.Range(0, positions.Length)];
    }
}
