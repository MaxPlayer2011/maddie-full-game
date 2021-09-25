using UnityEngine;

public class ItemPickup : MonoBehaviour
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
            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(gm.player.position, transform.position) < 10f)
                {
                    if (hit.transform.name == "Item_Soda")
                    {
                        AddItem(1);
                    }

                    else if (hit.transform.name == "Item_CandyBar")
                    {
                        AddItem(2);
                    }

                    else if (hit.transform.name == "Item_Scissors")
                    {
                        AddItem(3);
                    }

                    else if (hit.transform.name == "Item_Book")
                    {
                        AddItem(4);
                    }

                    else if (hit.transform.name == "Item_Log")
                    {
                        AddItem(5);
                    }

                    else if (hit.transform.name == "Item_Dollar")
                    {
                        AddItem(6);
                    }
                }
            }
        }
    }

    void AddItem(int id)
    {
        gm.AddItem(id);
        Destroy(gameObject);
        hover.MouseExit();
    }
}