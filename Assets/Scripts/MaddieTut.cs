using UnityEngine;
using GenericManagers;

public class MaddieTut : MonoBehaviour
{
    private GM_Tut gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM_Tut>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.GameOver();
        }
    }
}