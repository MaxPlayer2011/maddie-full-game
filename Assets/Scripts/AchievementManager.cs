using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementManager : MonoBehaviour
{
    public Animator anim;
    public TextMeshProUGUI achieveText;
    public Image image;
    public Sprite[] achieveSprite;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void CreateAchievement(string type)
    {
        if (PlayerPrefs.GetInt("debug") != 1)
        {
            if (PlayerPrefs.GetInt("ACH_" + type) == 0)
            {
                switch (type)
                {
                    case "StartGame":
                        SpawnAchievement("\"Oh, Hello There!\"", "Start the game", achieveSprite[0]);
                        break;

                    case "BeatGame":
                        SpawnAchievement("Mmm, Cake!", "Beat the game", achieveSprite[1]);
                        break;

                    case "SecretLevel":
                        SpawnAchievement("What the hell is this?", "Get the secret ending", achieveSprite[2]);
                        break;

                    case "PassTut":
                        SpawnAchievement("\"..will force you to listen to his crappy music for 5 seconds.\"", "Pass the tutorial", achieveSprite[3]);
                        break;

                    case "HardQ":
                        SpawnAchievement("Hey! This is impossible!", "Get the impossible question", achieveSprite[4]);
                        break;

                    case "DOS_Secret":
                        SpawnAchievement("It's so dark in here...", "Access \"SECRET.BAT\"", achieveSprite[5]);
                        break;

                    case "DOS_Doors":
                        SpawnAchievement("Doors 95? That sounds familiar...", "Open \"Doors 95\"", achieveSprite[6]);
                        break;

                    case "DOS_Doors_Fix":
                        SpawnAchievement("I don't care about \"DUMBCRAP\"! Just let me in!", "Open Doors 95 without getting an error", achieveSprite[7]);
                        break;
                }

                PlayerPrefs.SetInt("ACH_" + type, 1);
            }
        }
    }

    private void SpawnAchievement(string title, string text, Sprite sprite)
    {
        anim.Play("Spawn", -1, 0f);
        image.sprite = sprite;
        achieveText.text = "<u>Achievement Get!</u>\n<b>" + title + "</b>\n" + text;
    }
}
