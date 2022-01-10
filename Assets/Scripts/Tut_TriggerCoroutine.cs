using UnityEngine;
using GenericManagers;

public class Tut_TriggerCoroutine : MonoBehaviour
{
    private GM_Tut gm;
    public string coroutine;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM_Tut>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.StartCoroutine(coroutine);
            Destroy(gameObject);
        }
    }
}