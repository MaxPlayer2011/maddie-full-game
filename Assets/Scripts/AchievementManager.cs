using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementManager : MonoBehaviour
{
    private enum achieveType
    {
        StartGame,
        BeatGame,
        SecretLevel,
        PassTut,
        HardQ
    };
    public Animator anim;
    public TextMeshProUGUI achieveText;
    public Image image;
    public Sprite[] achieveSprite;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void CreateAchievement(string type)
    {
        if (PlayerPrefs.GetInt("ACH_" + type) == 0)
        {
            if (type == achieveType.StartGame.ToString())
            {
                SpawnAchievement("\"Oh, Hello There!\"", "Start the game", achieveSprite[0]);
            }

            PlayerPrefs.SetInt("ACH_" + type, 1);
        }
    }

    void SpawnAchievement(string title, string text, Sprite sprite)
    {
        anim.Play("Spawn");
        image.sprite = sprite;
        achieveText.text = "<u>Achievement Get!</u>\n<b>" + title + "</b>\n" + text;
    }
}