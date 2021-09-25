using UnityEngine;
using TMPro;

public class Lang_to_Lang_TEXT : MonoBehaviour
{
    private TextMeshProUGUI text;
    [TextArea]
    public string text_eng;
    [TextArea]
    public string text_rus;
    [TextArea]
    public string text_span;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        if (PlayerPrefs.GetString("lang") == "eng")
        {
            text.text = text_eng;
        }

        else if (PlayerPrefs.GetString("lang") == "rus")
        {
            text.text = text_rus;
        }

        else if (PlayerPrefs.GetString("lang") == "span")
        {
            text.text = text_span;
        }
    }
}