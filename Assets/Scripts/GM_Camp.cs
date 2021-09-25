using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GM_Camp : MonoBehaviour
{
    public float campfireLength;
    public float time;
    public float winEnd;
    public bool win;
    public bool spooky;
    public int points;
    private AudioSource audioSource;
    public AudioClip MAD_intro;
    public AudioClip winClip;
    public GameObject tutMaddie;
    public GameObject maddie;
    public GameObject sticks;
    public GameObject enemyNPC;
    public GameObject campfire;
    public GameObject campfireSprite;
    public Light campfireLight;
    public Transform player;
    public GameObject hud;
    public GameObject yay;
    public TextMeshProUGUI timeText;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("NoMaddie");
    }

    void Update()
    {
        time -= Time.deltaTime * 1f;
        timeText.text = Mathf.CeilToInt(time) + " seconds left";
        campfireLength -= Time.deltaTime * 0.01f;
        campfire.transform.localScale = Vector3.one * campfireLength;
        campfireLight.range = campfireLength * 20f;
        campfireLight.intensity = campfireLength * 5f;

        if (win == false)
        {
            audioSource.pitch = campfireLength;
        }

        if ((campfireLength < 0.5f) & spooky == false)
        {
            Spooky();
        }

        if (time < 0 & win == false)
        {
            Win();
        }

        if (win == true)
        {
            winEnd -= Time.unscaledDeltaTime * 1f;
        }

        if (winEnd < 0)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
            PlayerPrefs.SetInt("FT_Won", 1);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    IEnumerator NoMaddie()
    {
        yield return new WaitForSeconds(MAD_intro.length);
        tutMaddie.SetActive(false);
    }

    void Spooky()
    {
        spooky = true;
        maddie.SetActive(true);
        sticks.SetActive(false);
        enemyNPC.SetActive(false);
        campfireSprite.SetActive(false);
        campfireLight.gameObject.SetActive(false);
    }

    void Win()
    {
        win = true;
        Time.timeScale = 0f;
        audioSource.volume = 0.3f;
        audioSource.pitch = 1f;
        audioSource.clip = winClip;
        audioSource.Play();
        hud.SetActive(false);
        yay.SetActive(true);
    }
}