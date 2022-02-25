using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using GenericManagers;

public class PRI : MonoBehaviour
{
    public bool angry;
    public bool busy;
    public int detentionTime;
    private int nextDetentionTimer = 15;
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
    public AudioClip detentionIntroClip;
    public AudioClip[] detentionTimerClip;
    public string[] detentionTimerText;
    public AudioClip[] detentionWarnClip;
    public string[] detentionWarnText;
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

            if (!angry & !agent.isStopped)
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

        if (angry)
        {
            agent.SetDestination(gm.player.position);
        }

        if (detentionTime == 1)
        {
            nextDetentionTimer = 30;
        }

        else if (detentionTime == 2)
        {
            nextDetentionTimer = 60;
        }

        else if (detentionTime >= 3)
        {
            nextDetentionTimer = 120;
        }

        if (detentionTime > 4)
        {
            detentionTime = 4;
        }
        
        Vector3 direction = gm.player.position - transform.position;
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position + Vector3.up, direction, out hit, Mathf.Infinity, 3, QueryTriggerInteraction.Ignore) & hit.transform.tag == "Player" & !busy)
        {
            if (!gm.detention)
            {
                if (Input.GetKey(KeyCode.LeftShift) & timeToGetAngry > 0f & !gm.playerScript.outside & gm.playerScript.stamina.value > 0f & gm.playerScript.currentSpeed == gm.playerScript.sprintSpeed)
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

                if (gm.playerScript.guilty & !angry)
                {
                    angry = true;

                    if (gm.playerScript.guilt == "run")
                    {
                        PlayAudio(noRunning, "No running!... in the halls!");
                    }
                }
            }

            if (gm.playerScript.guilty & !angry & gm.detention)
            {
                angry = true;
                PlayAudio(noEscaping, "No escaping detention!... in the halls!");
            }
        }

        direction = gm.bullyScript.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, 3, QueryTriggerInteraction.Ignore) & hit.transform.tag == "Bully" & gm.bullyScript.spoken & !angry & !busy)
        {
            PlayAudio(noBullying, "No bullying students!... in the halls!");
            gm.bullyScript.Spawn(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player")) & angry)
        {
            gm.detention = true;
            gm.detentionTime = nextDetentionTimer;
            detentionTime++;
            gm.playerController.enabled = false;
            gm.player.transform.position = new Vector3(183f, 0f, 122f);
            gm.player.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gm.playerController.enabled = true;
            agent.Warp(new Vector3(183f, 0f, 110f));
            agent.isStopped = true;
            StartCoroutine("DetentionAudio");
            gm.maddieScript.Hear(transform.position);
            gm.detentionDoor.locked = true;
            timeToGetAngry = 0.5f;
            angry = false;
            gm.playerScript.guilty = false;
        }
    }

    void Wander()
    {
        if (!angry)
        {
            agent.SetDestination(gm.aiWanderPoints[Random.Range(0, gm.aiWanderPoints.Length)].position);
        }
    }

    void PlayAudio(AudioClip clip, string subtitle)
    {
        hum.Stop();
        audioSource.clip = clip;
        audioSource.Play();
        dialogeSystem.text.text = subtitle;
    }

    public void Distract()
    {
        busy = true;
        audioSource.Stop();
        timeToNormal = 30f;
        timeToGetAngry = 0.5f;
        angry = false;
        gm.playerScript.guilty = false;
        spriteRenderer.sprite = reading;
        agent.isStopped = true;
    }

    IEnumerator DetentionAudio()
    {
        PlayAudio(detentionIntroClip, "You get detention for");
        yield return new WaitForSeconds(detentionIntroClip.length + 0.25f);
        PlayAudio(detentionTimerClip[detentionTime - 1], detentionTimerText[detentionTime - 1]);
        yield return new WaitForSeconds(detentionTimerClip[detentionTime - 1].length + 0.25f);
        int random_warn = Random.Range(0, detentionWarnClip.Length);
        PlayAudio(detentionWarnClip[random_warn], detentionWarnText[random_warn]);
        yield return new WaitForSeconds(detentionWarnClip[random_warn].length);
        agent.isStopped = false;
    }
}