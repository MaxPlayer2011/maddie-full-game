using UnityEngine;

public class DarkHallTrigger : MonoBehaviour
{
    public Material lightSprite;
    public Material darkSprite;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SpriteRenderer>() != null)
        {
            other.GetComponent<SpriteRenderer>().material = darkSprite;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SpriteRenderer>() != null)
        {
            other.GetComponent<SpriteRenderer>().material = lightSprite;
        }
    }
}