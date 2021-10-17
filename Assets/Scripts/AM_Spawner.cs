using UnityEngine;

public class AM_Spawner : MonoBehaviour
{
    public GameObject am;
    
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("ACH_Canvas") == null)
        {
            Instantiate(am);
        }

        Destroy(gameObject);
    }
}