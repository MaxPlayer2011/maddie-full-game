using UnityEngine;

public class PLN_FixProblem : MonoBehaviour
{
    private GM_Plane gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM_Plane>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetButtonDown("Interact"))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "FixProblemTemp" & Vector3.Distance(gm.player.position, transform.position) < 10f)
                {
                    gm.FixProblem();
                }
            }
        }
    }
}