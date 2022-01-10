using UnityEngine;
using GenericManagers;

public class Door : MonoBehaviour
{
    public float closeCountdown;
    public bool opened;
    public bool locked;
    public bool mainGame;
    private GameManager gm;
    private GameObject player;
    private AudioSource audioSource;
    private MeshRenderer meshRenderer;
    private Collider Collider;
    public Material closed;
    public Material open;
    public AudioClip openClip;
    public AudioClip closeClip;

    void Start()
    {
        if (mainGame == true)
        {
            gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        }

        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        Collider = GetComponentInChildren<Collider>();
    }

    void Update()
    {
        if (closeCountdown > 0f)
        {
            closeCountdown -= 1 * Time.deltaTime;
        }

        if (closeCountdown < 0 & opened == true)
        {
            Close();
        }

        if (locked == true & opened == true)
        {
            Close();
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetButtonDown("Interact") & Time.timeScale == 1f)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Door" & Vector3.Distance(player.transform.position, transform.position) < 10f)
                {
                    if (opened == false & locked == false)
                    {
                        Open();
                    }
                }
            }
        }
    }

    public void Open()
    {
        opened = true;
        Collider.enabled = false;
        meshRenderer.material = open;
        closeCountdown = 3;
        audioSource.PlayOneShot(openClip, 0.5f);

        if (mainGame == true)
        {
            if (gm.maddieScript.gameObject.activeInHierarchy == true)
            {
                gm.maddieScript.Hear(transform.position);
            }
        }
    }

    public void Close()
    {
        opened = false;
        Collider.enabled = true;
        meshRenderer.material = closed;
        audioSource.PlayOneShot(closeClip, 0.5f);
    }
}