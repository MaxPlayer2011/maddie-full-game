using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MaddieCamp : MonoBehaviour
{
    private NavMeshAgent agent;
    private GM_Camp gm;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM_Camp>();
    }

    void Update()
    {
        agent.SetDestination(gm.player.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("BSOD02");
            PlayerPrefs.SetString("GameOver", "camp");
            PlayerPrefs.SetInt("campPoints", gm.points);
        }
    }
}