using UnityEngine;
using GenericManagers;

public class StartTrigger : MonoBehaviour
{
    private GameManager gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.welcome.Play();
            Destroy(gameObject);
        }
    }
}