using UnityEngine;

public class SodaSpray : MonoBehaviour
{
    public float speed;
    public float timeToDestroy;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    void Update()
    {
        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy < 0f)
        {
            Destroy(gameObject);
        }
    }
}