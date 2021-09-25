using UnityEngine;
using UnityEngine.AI;

public class SodaAffect : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Soda"))
        {
            agent.velocity = other.GetComponent<Rigidbody>().velocity;
        }
    }
}