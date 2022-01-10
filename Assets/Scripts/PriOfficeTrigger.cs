using UnityEngine;
using GenericManagers;

public class PriOfficeTrigger : MonoBehaviour
{
    private GameManager gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void OnTriggerStay(Collider other)
    {
        if (gm.detention == true)
        {
            gm.playerScript.guilty = false;
            gm.playerScript.guilt = null;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (gm.detention == true)
        {
            gm.playerScript.guilty = true;
            gm.playerScript.guilt = "escape";
        }
    }
}