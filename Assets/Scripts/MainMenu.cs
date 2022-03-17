using System.Collections;
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
    public Slider progressBar;
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

    private void Start()
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

        switch (PlayerPrefs.GetString("lang"))
        {
            case "eng": langValue = 0;
                break;
            case "rus": langValue = 3;
                break;
            case "pol": langValue = 2;
                break;
            case "span": langValue = 1;
                break;
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

    private void Update()
    {
        if (reseting)
        {
            PlayerPrefs.DeleteAll();
        }

        else
        {
            PlayerPrefs.SetFloat("mouseSensitivity", slider.value);

            if (subtitleToggle.isOn)
            {
                PlayerPrefs.SetInt("subtitle", 1);
            }

            else
            {
                PlayerPrefs.SetInt("subtitle", 0);
            }

            switch (lang.value)
            {
                case 0:
                    PlayerPrefs.SetString("lang", "eng");
                    langText.text = "LANGUAGE";
                    break;
                case 3:
                    PlayerPrefs.SetString("lang", "rus");
                    langText.text = "ЯЗЫК";
                    break;
                case 2:
                    PlayerPrefs.SetString("lang", "pol");
                    langText.text = "JĘZYK";
                    break;
                case 1:
                    PlayerPrefs.SetString("lang", "span");
                    langText.text = "IDIOMA";
                    break;
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

        if (mainScreen.activeInHierarchy)
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
        StartCoroutine(ProgressBar(scene));
    }

    private IEnumerator ProgressBar(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        while (!operation.isDone)
        {
            progressBar.value = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
