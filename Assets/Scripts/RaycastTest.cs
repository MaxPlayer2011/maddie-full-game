using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.name);
        }
    }
}