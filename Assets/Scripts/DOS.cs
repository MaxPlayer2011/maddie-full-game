using UnityEngine;
using UnityEngine.SceneManagement;
using GenericManagers.GUI;
using TMPro;

public class DOS : MonoBehaviour
{
    private bool gameWantsToLaunch;
    private bool screen;
    private float timeToLaunchGame = 3f;
    public GameObject textParent;
    public GameObject hintObject;
    public GameObject badCommand;
    public GameObject secret;
    public GameObject secretTxt;
    public GameObject errorMessage;
    public GameObject drs95;
    public GameObject text;
    public TMP_InputField inputField;
    public AchievementManager am;
    private AudioSource drs95Audio;
    public AudioClip chimes;
    public AudioClip ding;
    public AudioClip tada;

    private void Start()
    {
        CursorManager.Lock();
        drs95Audio = drs95.GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!screen)
            {
                switch (inputField.text.ToLower())
                {
                    case "maddie.exe":
                        gameWantsToLaunch = true;
                        am.CreateAchievement("StartGame");
                        break;
                    case "help":
                        screen = true;
                        textParent.SetActive(false);
                        hintObject.SetActive(true);
                        break;
                    case "secret.txt":
                        screen = true;
                        textParent.SetActive(false);
                        secretTxt.SetActive(true);
                        break;
                    case "secret.bat":
                        screen = true;
                        textParent.SetActive(false);
                        secret.SetActive(true);
                        am.CreateAchievement("DOS_Secret");
                        break;
                    case "doors":
                        screen = true;
                        textParent.SetActive(false);
                        errorMessage.SetActive(true);
                        am.CreateAchievement("DOS_Doors");
                        break;
                    case "doors --ignore dumbcrap.sys":
                        screen = true;
                        textParent.SetActive(false);
                        drs95.SetActive(true);
                        CursorManager.Unlock();
                        am.CreateAchievement("DOS_Doors_Fix");
                        drs95Audio.clip = chimes;
                        drs95Audio.Play();
                        break;
                    default:
                        screen = true;
                        textParent.SetActive(false);
                        badCommand.SetActive(true);
                        break;
                }
            }

            else
            {
                if (!drs95.activeInHierarchy)
                {
                    screen = false;
                    textParent.SetActive(true);
                    hintObject.SetActive(false);
                    badCommand.SetActive(false);
                    secret.SetActive(false);
                    secretTxt.SetActive(false);
                    errorMessage.SetActive(false);
                }
            }

            if (!gameWantsToLaunch)
                inputField.text = null;
        }

        if (!gameWantsToLaunch)
            inputField.Select();

        else
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            timeToLaunchGame -= Time.deltaTime;
            text.SetActive(true);
        }

        if (timeToLaunchGame < 0f)
            SceneManager.LoadScene("Logo");

        if (Input.anyKeyDown & drs95.activeInHierarchy & !drs95Audio.isPlaying)
            if (!(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
                drs95Audio.PlayOneShot(ding);
    }

    public void ExitDoors95()
    {
        drs95Audio.PlayOneShot(tada);
        screen = false;
        textParent.SetActive(true);
        hintObject.SetActive(false);
        badCommand.SetActive(false);
        secret.SetActive(false);
        secretTxt.SetActive(false);
        errorMessage.SetActive(false);
        drs95.SetActive(false);
        CursorManager.Lock();
    }
}
