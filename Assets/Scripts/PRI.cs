using UnityEngine;
using UnityEngine.AI;

public class PRI : MonoBehaviour
{
    public bool angry;
    public bool busy;
    public float timeToHum;
    public float timeToGetAngry;
    public float timeToNormal;
    private NavMeshAgent agent;
    private GameManager gm;
    private Subtitle dialogeSystem;
    private Subtitle dialogeSystemHum;
    private AudioSource audioSource;
    public AudioSource hum;
    private SpriteRenderer spriteRenderer;
    public Sprite normal;
    public Sprite reading;
    public AudioClip detentionClip;
    public AudioClip noRunning;
    public AudioClip noEscaping;
    public AudioClip noBullying;
    public AudioClip humClip;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        dialogeSystem = GetComponent<Subtitle>();
        dialogeSystemHum = GetComponentInChildren<Subtitle>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Wander();
    }

    void Update()
    {
        timeToHum -= Time.deltaTime;

        if (timeToHum < 0f)
        {
            timeToHum = Random.Range(500, 620);

            if (!angry)
            {
                hum.PlayOneShot(humClip, 2f);
                dialogeSystemHum.text.text = "*Humming*";
            }
        }

        if (timeToNormal > 0f)
        {
            timeToNormal -= Time.deltaTime;
        }

        if (timeToNormal < 0f)
        {
            busy = false;
            spriteRenderer.sprite = normal;
            agent.isStopped = false;
        }

        if (agent.remainingDistance < 1)
        {
            Wander();
        }

        if (angry == true)
        {
            agent.SetDestination(gm.player.position);
        }
        
        Vector3 direction = gm.player.position - transform.position;
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position + Vector3.up, direction, out hit, Mathf.Infinity, 3, QueryTriggerInteraction.Ignore) & hit.transform.tag == "Player" & !busy)
        {
            if (gm.detention == false)
            {
                if (Input.GetKey(KeyCode.LeftShift) & timeToGetAngry > 0f & !gm.playerScript.outside & gm.playerScript.stamina.value > 0f)
                {
                    timeToGetAngry -= Time.deltaTime * 1f;
                }

                else
                {
                    if (timeToGetAngry > 0f)
                    {
                        timeToGetAngry = 0.5f;
                    }
                }

                if (timeToGetAngry <= 0f)
                {
                    gm.playerScript.guilty = true;
                }

                if (gm.playerScript.guilty == true & angry == false)
                {
                    angry = true;

                    if (gm.playerScript.guilt == "run")
                    {
                        PlayRuleBreakAudio(noRunning, "No running!... in the halls!");
                    }
                }
            }

            if (gm.playerScript.guilty & !angry & gm.detention)
            {
                angry = true;
                PlayRuleBreakAudio(noEscaping, "No escaping detention!... in the halls!");
            }
        }

        direction = gm.bullyScript.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, 3, QueryTriggerInteraction.Ignore) & hit.transform.tag == "Bully" & gm.bullyScript.spoken & !angry & !busy)
        {
            PlayRuleBreakAudio(noBullying, "No bullying students!... in the halls!");
            gm.bullyScript.Spawn(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player")) & angry)
        {
            gm.detention = true;
            gm.detentionTime = 15f;
            gm.playerController.enabled = false;
            gm.player.transform.position = new Vector3(183f, 0f, 122f);
            gm.player.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gm.playerController.enabled = true;
            agent.Warp(new Vector3(183f, 0f, 110f));
            PlayRuleBreakAudio(detentionClip, "15 Seconds!... Detention for you!");
            gm.maddieScript.Hear(transform.position);
            gm.detentionDoor.locked = true;
            timeToGetAngry = 0.5f;
            angry = false;
            gm.playerScript.guilty = false;
        }
    }

    void Wander()
    {
        if (angry == false)
        {
            agent.SetDestination(gm.aiWanderPoints[Random.Range(0, gm.aiWanderPoints.Length)].position);
        }
    }

    void PlayRuleBreakAudio(AudioClip clip, string subtitle)
    {
        hum.Stop();
        audioSource.clip = clip;
        audioSource.Play();
        dialogeSystem.text.text = subtitle;
    }

    public void Distract()
    {
        busy = true;
        timeToNormal = 30f;
        timeToGetAngry = 0.5f;
        angry = false;
        gm.playerScript.guilty = false;
        spriteRenderer.sprite = reading;
        agent.isStopped = true;
    }
}