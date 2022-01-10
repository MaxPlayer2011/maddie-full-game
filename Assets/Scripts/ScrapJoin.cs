using UnityEngine;
using GenericManagers;

public class ScrapJoin : MonoBehaviour
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
                if (hit.transform.name == "join" & Vector3.Distance(gm.player.position, transform.position) < 20f)
                {
                    if (gm.exitsReached == 4)
                    {
                        gm.ScrapJoin();
                    }
                }
            }
        }
    }
}