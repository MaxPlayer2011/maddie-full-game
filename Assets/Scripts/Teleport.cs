using UnityEngine;
using GenericManagers;

public class Teleport : MonoBehaviour
{
    private GameManager gm;
    
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetButtonDown("Interact"))
        {
            if ((Physics.Raycast(ray, out hit)) & Time.timeScale == 1f)
            {
                if (hit.transform.name == "TeleportEntrance" & Vector3.Distance(gm.player.position, transform.position) < 10f)
                {
                    gm.playerController.enabled = false;
                    gm.player.transform.position = new Vector3(5f, 0f, 15f);
                    gm.player.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    gm.playerController.enabled = true;
                }

                if (hit.transform.name == "TeleportPHRoom" & Vector3.Distance(gm.player.position, transform.position) < 10f)
                {
                    gm.playerController.enabled = false;
                    gm.player.transform.position = new Vector3(-100f, 0f, 50f);
                    gm.player.localRotation = Quaternion.Euler(0f, 270f, 0f);
                    gm.playerController.enabled = true;
                }
            }
        }
    }
}