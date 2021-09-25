using UnityEngine;

public class Scrap : MonoBehaviour
{
    private GameManager gm;
    private Hover hover;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        hover = GetComponent<Hover>();
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
                    Instantiate(gm.math);
                    gm.learning = true;
                    Time.timeScale = 0f;
                    gm.scraps += 1;
                    Destroy(hit.transform.gameObject);
                    hover.MouseExit();
                }
            }
        }
    }
}