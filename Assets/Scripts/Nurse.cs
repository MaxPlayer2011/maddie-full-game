using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Nurse : MonoBehaviour
{
    public bool inOffice;
    public bool helping;
    public string[] problemText;
    public string[] fixText;
    public string[] doneText;
    private AudioSource audioSource;
    public AudioClip hello;
    public AudioClip[] problem;
    public AudioClip hallucination;
    public AudioClip[] fix;
    public AudioClip[] done;
    private AudioClip currentRandomClip;
    private NavMeshAgent agent;
    public Transform[] wanderPoints;
    public Player player;
    public Door door;
    private Subtitle dialogeSystem;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        dialogeSystem = GetComponent<Subtitle>();
        Wander();
    }
    
    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            Wander();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & helping == false & inOffice == true & player.crazy == true)
        {
            StartCoroutine("Help");
        }
    }

    void Wander()
    {
        agent.SetDestination(wanderPoints[Random.Range(0, wanderPoints.Length)].position);
    }

    IEnumerator Help()
    {
        helping = true;
        agent.isStopped = true;
        door.enabled = false;
        audioSource.PlayOneShot(hello);
        dialogeSystem.text.text = "Hello";
        yield return new WaitForSeconds(hello.length);
        int randomNumberProblem = Random.Range(0, problem.Length);
        currentRandomClip = problem[randomNumberProblem];
        audioSource.PlayOneShot(currentRandomClip);
        dialogeSystem.text.text = problemText[randomNumberProblem];
        yield return new WaitForSeconds(currentRandomClip.length);
        audioSource.PlayOneShot(hallucination);
        dialogeSystem.text.text = "Hallucination!";
        yield return new WaitForSeconds(hallucination.length);
        int randomNumberFix = Random.Range(0, fix.Length);
        currentRandomClip = fix[randomNumberFix];
        audioSource.PlayOneShot(currentRandomClip);
        dialogeSystem.text.text = fixText[randomNumberFix];
        yield return new WaitForSeconds(currentRandomClip.length + 5f);
        int randomNumberDone = Random.Range(0, done.Length);
        currentRandomClip = done[randomNumberDone];
        audioSource.PlayOneShot(currentRandomClip);
        dialogeSystem.text.text = doneText[randomNumberDone];
        yield return new WaitForSeconds(currentRandomClip.length);
        helping = false;
        agent.isStopped = false;
        door.enabled = true;
        player.timeToCrazy = Random.Range(300f, 420f);
    }
}