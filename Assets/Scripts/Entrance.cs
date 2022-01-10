using UnityEngine;
using GenericManagers;

public class Entrance : MonoBehaviour
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
            if (gm.exitsReached < 4)
            {
                transform.parent.position = new Vector3(transform.parent.position.x, 20f, transform.parent.position.z);
                gm.ExitReached();
                gm.maddieScript.Hear(transform.position);
            }
        }
    }
}