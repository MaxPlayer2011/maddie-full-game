using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private int langValue;
    private bool reseting;
    private AudioSource audioSource;
    public TextMeshProUGUI tut;
    public TextMeshProUGUI langText;
    public Slider slider;
    public Toggle subtitleToggle;
    public TMP_Dropdown lang;
    public GameObject mainScreen;
    public GameObject changelog;
    public GameObject debug;
    public GameObject mainMenuFullScreen;
    public TextMeshProUGUI debugText;
    public TextMeshProUGUI versionText;
    public TMP_InputField sceneInput;
    public Sprite wonScreen;
    public FPSCounter fps;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

        if (PlayerPrefs.GetString("lastVersion") != Application.version)
        {
            changelog.SetActive(true);
        }

        if (PlayerPrefs.GetInt("won") == 1)
        {
            mainScreen.GetComponent<Image>().sprite = wonScreen;
            versionText.color = Color.white;
            PlayerPrefs.SetInt("won", 0);
        }

        else
        {
            audioSource.Play();
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
            mainMenuFullScreen.SetActive(true);
        }

        else
        {
            mainMenuFullScreen.SetActive(false);
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

    public void ExitChangelog()
    {
        PlayerPrefs.SetString("lastVersion", Application.version);
    }

    public void ResetGame()
    {
        reseting = true;
        SceneManager.LoadScene(0);
    }
}