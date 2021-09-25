using UnityEngine;

public class Bus : MonoBehaviour
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
            gm.GameOverEnd("bus");
        }
    }
}