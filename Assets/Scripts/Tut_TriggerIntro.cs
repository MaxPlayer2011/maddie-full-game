using UnityEngine;

public class Tut_TriggerIntro : MonoBehaviour
{
    private GM_Tut gm;
    public MeshRenderer meshRenderer;
    public Material close;
    public AudioSource audioSource;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM_Tut>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.StartCoroutine("Intro");
            meshRenderer.material = close;
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}