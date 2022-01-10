using UnityEngine;
using UnityEngine.AI;
using GenericManagers;

public class Maddie : MonoBehaviour
{
    public int anger;
    public float timeToScare;
    public float timeToCutLog;
    public float timeToStopCutLog;
    public float antiHearingTime;
    public string[] scareText;
    public bool playerSeen;
    public bool preparing;
    public bool cutting;
    public bool antiHearing;
    private SpriteRenderer spriteRenderer;
    public Material lightSprite;
    private Animator anim;
    public Sprite normal;
    public Sprite prepare;
    private AudioSource audioSource;
    public AudioClip[] mad;
    public AudioClip[] scare;
    public AudioClip logClip;
    public AudioClip chainsaw;
    private NavMeshAgent agent;
    private GameManager gm;
    private Subtitle dialogeSystem;
    public GameObject hear;
    public GameObject log;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        dialogeSystem = GetComponent<Subtitle>();
        Wander();
    }

    void Update()
    {
        agent.speed = anger;
        timeToScare -= Time.deltaTime;

        if (agent.remainingDistance < 1)
        {
            Wander();
        }

        Vector3 direction = gm.player.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up, direction, out hit, Mathf.Infinity, 3, QueryTriggerInteraction.Ignore) & hit.transform.CompareTag("Player") & !antiHearing)
        {
            playerSeen = true;
            agent.SetDestination(gm.player.position);
        }

        else
        {
            playerSeen = false;
        }

        if (timeToScare < 0f)
        {
            timeToScare = Random.Range(60, 120);
            int random = Random.Range(0, scare.Length);
            audioSource.PlayOneShot(scare[random], 2f);
            dialogeSystem.text.text = scareText[random];
        }

        if (timeToCutLog > 0f)
        {
            timeToCutLog -= Time.deltaTime;
        }

        if (timeToCutLog < 0f & preparing)
        {
            preparing = false;
            cutting = true;
            timeToStopCutLog = 30f;
            anim.enabled = true;
            audioSource.loop = true;
            audioSource.clip = chainsaw;
            audioSource.Play();
            dialogeSystem.text.text = "*Loud chainsaw noises*";
        }

        if (timeToStopCutLog > 0f)
        {
            timeToStopCutLog -= Time.deltaTime;
        }

        if (timeToStopCutLog < 0f & cutting)
        {
            cutting = false;
            anim.enabled = false;
            spriteRenderer.sprite = normal;
            spriteRenderer.material = lightSprite;
            log.SetActive(false);
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.clip = null;
            agent.isStopped = false;
        }

        if (antiHearingTime > 0f)
        {
            antiHearingTime -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & !preparing & !cutting & !antiHearing)
        {
            gm.GameOver();
        }
    }

    void Wander()
    {
        if (!playerSeen)
        {
            agent.SetDestination(gm.aiWanderPoints[Random.Range(0, gm.aiWanderPoints.Length)].position);
        }
    }

    public void Hear(Vector3 location)
    {
        if (!antiHearing)
        {
            agent.SetDestination(location);
            Instantiate(hear);
        }
    }

    public void CutLog()
    {
        preparing = true;
        agent.isStopped = true;
        log.SetActive(true);
        audioSource.PlayOneShot(logClip);
        dialogeSystem.text.text = "Ooh! A log! Don't mind if I do!";
        spriteRenderer.sprite = prepare;
        timeToCutLog = logClip.length;
    }
}