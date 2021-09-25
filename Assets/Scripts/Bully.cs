using UnityEngine;

public class Bully : MonoBehaviour
{
    public float spawnTime;
    public float boreTime;
    public bool active;
    public bool spoken;
    public string[] acceptText;
    public string[] wantText;
    private GameManager gm;
    private Subtitle dialogeSystem;
    public Subtitle dialogeSystem2;
    private AudioSource audioSource;
    public AudioSource highAudio;
    public AudioClip[] accept;
    public AudioClip[] reject;
    public AudioClip[] want;
    public AudioClip bore;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        dialogeSystem = GetComponent<Subtitle>();
        audioSource = GetComponent<AudioSource>();
        spawnTime = Random.Range(120, 180);
    }

    void Update()
    {
        if (spawnTime > 0f & gm.spooky)
        {
            spawnTime -= Time.deltaTime;
        }

        if (spawnTime < 0f & !active)
        {
            Spawn(true);
        }

        if (boreTime > 0f)
        {
            boreTime -= Time.deltaTime;
        }

        if (boreTime < 0f & active)
        {
            highAudio.PlayOneShot(bore);
            dialogeSystem2.text.text = "I'm so bored...";
            Despawn();
        }

        Vector3 direction = gm.player.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up, direction, out hit, 15f, 3, QueryTriggerInteraction.Ignore) & !spoken & active & GetComponentInChildren<SpriteRenderer>().isVisible)
        {
            if (hit.transform.tag == "Player" & hit.transform != null)
            {
                spoken = true;
                int randomClip = Random.Range(0, want.Length);
                audioSource.PlayOneShot(want[randomClip]);
                dialogeSystem.text.text = wantText[randomClip];
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gm.items[0] == 0 & gm.items[1] == 0 & gm.items[2] == 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(reject[Random.Range(0, reject.Length)]);
                dialogeSystem.text.text = "Oh, you don't have items. Well then, no pass!";
            }
        }

        else
        {
            gm.items[Random.Range(0, 2)] = 0;
            gm.UpdateInventoryData();
            int randomClip = Random.Range(0, accept.Length);
            highAudio.PlayOneShot(accept[randomClip]);
            dialogeSystem2.text.text = acceptText[randomClip];
            Despawn();
        }
    }

    public void Spawn(bool random)
    {
        active = true;
        spoken = false;
        spawnTime = 0f;
        boreTime = Random.Range(300, 360);

        if (random)
        {
            transform.position = gm.bullySpawnPoints[Random.Range(0, gm.bullySpawnPoints.Length)].position;
        }

        else
        {
            transform.position = new Vector3(170f, 0f, 130f);
        }
    }

    void Despawn()
    {
        active = false;
        spoken = false;
        spawnTime = Random.Range(120, 180);
        transform.position = new Vector3(transform.position.x, -10, transform.position.z);
    }
}