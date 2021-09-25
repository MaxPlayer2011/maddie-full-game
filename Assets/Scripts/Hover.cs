using UnityEngine;
using UnityEngine.UI;

public class Hover : MonoBehaviour
{
    private Image crosshair_hover;
    
    void Start()
    {
        crosshair_hover = GameObject.Find("CrosshairHover").GetComponent<Image>();
    }

    void OnMouseOver()
    {
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < 10f)
        {
            crosshair_hover.color = new Color(255, 255, 255, 255);
        }

        else
        {
            MouseExit();
        }
    }

    void OnMouseExit()
    {
        MouseExit();
    }

    public void MouseExit()
    {
        crosshair_hover.color = new Color(255, 255, 255, 0);
    }
}