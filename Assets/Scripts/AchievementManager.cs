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
        HardQ,
        DOS_Secret,
        DOS_Doors
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

            else if (type == achieveType.BeatGame.ToString())
            {
                SpawnAchievement("Mmm, Cake!", "Beat the game", achieveSprite[1]);
            }

            else if (type == achieveType.SecretLevel.ToString())
            {
                SpawnAchievement("What the hell is this?", "Get the secret ending", achieveSprite[2]);
            }

            else if (type == achieveType.PassTut.ToString())
            {
                SpawnAchievement("\"..will force you to listen to his crappy music for 5 seconds.\"", "Pass the tutorial", achieveSprite[3]);
            }

            else if (type == achieveType.HardQ.ToString())
            {
                SpawnAchievement("Hey! This is impossible!", "Get the impossible question", achieveSprite[4]);
            }

            else if (type == achieveType.DOS_Secret.ToString())
            {
                SpawnAchievement("It's so dark in here...", "Access \"SECRET.BAT\"", achieveSprite[5]);
            }

            else if (type == achieveType.DOS_Doors.ToString())
            {
                SpawnAchievement("Doors 95? That sounds familiar...", "Open \"Doors 95\"", achieveSprite[6]);
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