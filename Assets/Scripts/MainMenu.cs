using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private int langValue;
    private bool reseting;
    public TextMeshProUGUI tut;
    public TextMeshProUGUI langText;
    public Slider slider;
    public Toggle subtitleToggle;
    public TMP_Dropdown lang;
    public GameObject mainScreen;
    public GameObject FT_Congrats;
    public GameObject debug;
    public GameObject langButton;
    public TextMeshProUGUI debugText;
    public TextMeshProUGUI versionText;
    public TMP_InputField sceneInput;
    public FPSCounter fps;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("mouseSensitivity");

        if (PlayerPrefs.GetInt("subtitle") == 1)
        {
            subtitleToggle.isOn = true;
        }

        else
        {
            subtitleToggle.isOn = false;
        }

        versionText.text = "V" + Application.version;

        if (PlayerPrefs.GetString("lang") == "eng")
        {
            langValue = 0;
        }

        else if (PlayerPrefs.GetString("lang") == "rus")
        {
            langValue = 3;
        }

        else if (PlayerPrefs.GetString("lang") == "pol")
        {
            langValue = 2;
        }

        else if (PlayerPrefs.GetString("lang") == "span")
        {
            langValue = 1;
        }

        lang.value = langValue;

        if (slider.value == 1f)
        {
            slider.value = 3f;
        }

        if (PlayerPrefs.GetInt("FT_Won") == 1)
        {
            FT_Congrats.SetActive(true);
            PlayerPrefs.SetInt("FT_Won", 0);
        }
    }

    void Update()
    {
        if (reseting == true)
        {
            PlayerPrefs.DeleteAll();
        }

        else
        {
            PlayerPrefs.SetFloat("mouseSensitivity", slider.value);

            if (subtitleToggle.isOn == true)
            {
                PlayerPrefs.SetInt("subtitle", 1);
            }

            else
            {
                PlayerPrefs.SetInt("subtitle", 0);
            }

            if (lang.value == 0)
            {
                PlayerPrefs.SetString("lang", "eng");
                langText.text = "LANGUAGE";
            }

            else if (lang.value == 3)
            {
                PlayerPrefs.SetString("lang", "rus");
                langText.text = "ЯЗЫК";
            }

            else if (lang.value == 2)
            {
                PlayerPrefs.SetString("lang", "pol");
                langText.text = "JĘZYK";
            }

            else if (lang.value == 1)
            {
                PlayerPrefs.SetString("lang", "span");
                langText.text = "IDIOMA";
            }

            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                if (PlayerPrefs.GetInt("debug") == 0)
                {
                    PlayerPrefs.SetInt("debug", 1);
                }

                else
                {
                    PlayerPrefs.SetInt("debug", 0);
                }
            }
        }

        debugText.text = "DEBUG\nUnity Version: " + Application.unityVersion + ",\nFPS: " + fps.m_CurrentFps + ",\nTo disable debug, click \"~\"";

        if (mainScreen.activeInHierarchy == true)
        {
            langButton.SetActive(true);
        }

        else
        {
            langButton.SetActive(false);
        }

        if (PlayerPrefs.GetInt("debug") == 1)
        {
            debug.SetActive(true);
        }

        else
        {
            debug.SetActive(false);
        }
    }

    public void ButtonEnter(TextMeshProUGUI text)
    {
        text.fontStyle = FontStyles.Underline;
    }

    public void ButtonExit(TextMeshProUGUI text)
    {
        text.fontStyle = FontStyles.Normal;
    }

    public void TitleChange(string textToShow)
    {
        tut.text = textToShow;
    }

    public void Scene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void InputScene()
    {
        string scene = sceneInput.text;

        if (Application.CanStreamedLevelBeLoaded(scene))
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void fnf()
    {
        Application.OpenURL("https://ninja-muffin24.itch.io/funkin");
    }

    public void Kickstarter()
    {
        Application.OpenURL("");
    }

    public void ResetGame()
    {
        reseting = true;
        SceneManager.LoadScene(0);
    }
}