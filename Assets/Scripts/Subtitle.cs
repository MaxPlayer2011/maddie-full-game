using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Subtitle : MonoBehaviour
{
    [TextArea]
    public string dialoge;
    public bool multiplySubtitlePosition;
    private bool hearable;
    private AudioSource audioSource;
    private RectTransform subtitleRect;
    public Image subtitle;
    public TextMeshProUGUI text;
    public Canvas hud;
    private GameObject player;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        subtitleRect = subtitle.GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player");
        text.text = dialoge;
    }

    void Update()
    {
        float distance = (player.transform.position - transform.position).magnitude;
        float m = 10;
        subtitleRect.localScale = new Vector3(1 / distance * m, 1 / distance * m, 1);

        Vector3 subtitlePosition;
        Vector3 multiply;

        if (multiplySubtitlePosition == true)
        {
            multiply = new Vector3(0f, 5f, 0f);
        }

        else
        {
            multiply = new Vector3(0f, 0f, 0f);
        }

        subtitlePosition = Camera.main.WorldToScreenPoint(transform.position + multiply);

        if (subtitlePosition.z < 0)
        {
            subtitle.gameObject.SetActive(false);
        }

        else
        {
            if (audioSource.isPlaying == true & PlayerPrefs.GetInt("subtitle") == 1 & hearable == true)
            {
                subtitle.gameObject.SetActive(true);
            }

            else
            {
                subtitle.gameObject.SetActive(false);
            }
        }

        if (audioSource.isPlaying == false)
        {
            subtitle.gameObject.SetActive(false);
        }

        if (Vector3.Distance(player.transform.position, transform.position) <= audioSource.maxDistance)
        {
            hearable = true;
        }

        else
        {
            hearable = false;
        }

        float xMin = subtitle.rectTransform.rect.width / 2;
        float xMax = hud.pixelRect.width - subtitle.rectTransform.rect.width / 2;
        float yMin = subtitle.rectTransform.rect.height / 2;
        float yMax = hud.pixelRect.height - subtitle.rectTransform.rect.height / 2;

        subtitlePosition = new Vector3(Mathf.Clamp(subtitlePosition.x, xMin, xMax), Mathf.Clamp(subtitlePosition.y, yMin, yMax), subtitlePosition.z);
        subtitle.transform.position = subtitlePosition;
    }
}