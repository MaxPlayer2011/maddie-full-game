using UnityEngine;

public class BSOD_Teleport : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().TeleportPlayer(new Vector3(0f, 0f, 0f), Quaternion.identity);
        }
    }
}