using UnityEngine;
using UnityEngine.UI;

public class Lang_to_Lang_IMAGE : MonoBehaviour
{
    private Image image;
    public Sprite image_eng;
    public Sprite image_rus;
    public Sprite image_span;

    void Start()
    {
        image = GetComponent<Image>();

        if (PlayerPrefs.GetString("lang") == "eng")
        {
            image.sprite = image_eng;
            image.SetNativeSize();
        }

        else if (PlayerPrefs.GetString("lang") == "rus")
        {
            image.sprite = image_rus;
            image.SetNativeSize();
        }

        else if (PlayerPrefs.GetString("lang") == "span")
        {
            image.sprite = image_span;
            image.SetNativeSize();
        }
    }
}