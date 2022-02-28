using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Nurse : MonoBehaviour
{
    public bool inOffice;
    public bool helping;
    public bool patrolling;
    public string[] problemText;
    public string[] fixText;
    public string[] doneText;
    public AudioClip hello;
    public AudioClip[] problem;
    public AudioClip hallucination;
    public AudioClip[] fix;
    public AudioClip[] done;
    private NavMeshAgent agent;
    public Transform[] wanderPoints;
    public Player player;
    public Door door;
    private AudioQueueManager audioQueue;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioQueue = gameObject.AddComponent<AudioQueueManager>();
        StartCoroutine(Wander());
    }
    
    void Update()
    {
        if (agent.remainingDistance < 1 & !helping & !patrolling)
        {
            StartCoroutine(Wander());
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") & !helping & inOffice & player.crazy)
        {
            StartCoroutine(Help());
        }
    }

    IEnumerator Wander()
    {
        patrolling = true;

        if (inOffice)
        {
            yield return new WaitForSeconds(60f);
        }

        else
        {
            yield return new WaitForSeconds(15f);
        }

        patrolling = false;
        agent.SetDestination(wanderPoints[Random.Range(0, wanderPoints.Length)].position);
    }

    IEnumerator Help()
    {
        int randomNumberProblem = Random.Range(0, problem.Length);
        int randomNumberFix = Random.Range(0, fix.Length);
        int randomNumberDone = Random.Range(0, done.Length);

        helping = true;
        agent.isStopped = true;
        door.locked = true;
        audioQueue.Queue(hello, "Hello");
        audioQueue.Queue(problem[randomNumberProblem], problemText[randomNumberProblem]);
        audioQueue.Queue(hallucination, "Hallucination!");
        audioQueue.Queue(fix[randomNumberFix], fixText[randomNumberFix]);
        yield return new WaitForSeconds(hello.length + problem[randomNumberProblem].length + hallucination.length + fix[randomNumberFix].length + 5f);
        audioQueue.Queue(done[randomNumberDone], doneText[randomNumberDone]);
        player.timeToCrazy = Random.Range(300f, 420f);
        yield return new WaitForSeconds(fix[randomNumberDone].length);
        helping = false;
        agent.isStopped = false;
        door.locked = false;
    }
}