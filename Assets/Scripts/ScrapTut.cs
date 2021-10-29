using UnityEngine;

public class ScrapTut : MonoBehaviour
{
    private GM_Tut gm;
    public GameObject math;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM_Tut>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetButtonDown("Interact"))
        {
            if (Physics.Raycast(ray, out hit) & Time.timeScale == 1f)
            {
                if (hit.transform.tag == "Scrap" & Vector3.Distance(gm.player.position, transform.position) < 10f)
                {
                    Instantiate(math);
                    Time.timeScale = 0f;
                    gm.scraps = 1;
                    Destroy(hit.transform.gameObject);
                    GetComponent<Hover>().MouseExit();
                }
            }
        }
    }
}