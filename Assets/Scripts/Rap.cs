using UnityEngine;
using UnityEngine.AI;

public class Rap : MonoBehaviour
{
    public bool playerSeen;
    public float cooldown;
    public float timeToRecover;
    private GameManager gm;
    private SpriteRenderer spriteRenderer;
    public NavMeshAgent agent;
    public Sprite normal;
    public Sprite cut;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Wander();
    }

    void Update()
    {
        if (cooldown >= 0 & gm.playerScript.rap == false & Time.timeScale == 1f)
        {
            cooldown -= Time.deltaTime;
        }

        if (timeToRecover > 0f)
        {
            timeToRecover -= Time.deltaTime;
        }

        if (timeToRecover < 0f)
        {
            spriteRenderer.sprite = normal;
        }

        if (agent.remainingDistance < 1)
        {
            Wander();
        }

        Vector3 direction = gm.player.position - transform.position;
        RaycastHit hit;
        
        if (gm.playerScript.rap == false)
        {
            if (Physics.Raycast(transform.position + Vector3.up * 2f, direction, out hit, float.PositiveInfinity, 3, QueryTriggerInteraction.Ignore) & hit.transform.tag == "Player")
            {
                Chase();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (cooldown < 0)
            {
                gm.playerScript.rap = true;
                gm.rapTime = 5f;
                cooldown = 30f;
                agent.isStopped = true;
            }
        }
    }

    void Chase()
    {
        if (cooldown < 0)
        {
            agent.SetDestination(gm.player.position);
            playerSeen = true;
        }

        else
        {
            playerSeen = false;
        }
    }

    void Wander()
    {
        if (playerSeen == false)
        {
            agent.SetDestination(gm.aiWanderPoints[Random.Range(0, gm.aiWanderPoints.Length)].position);
        }
    }

    public void Cut()
    {
        gm.rapTime = -1f;
        spriteRenderer.sprite = cut;
        timeToRecover = 30f;
        cooldown = 30f;
    }
}