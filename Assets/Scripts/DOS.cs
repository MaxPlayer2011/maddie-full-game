using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
    public GameObject text;
    public TMP_InputField inputField;
    public AchievementManager am;
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!screen)
            {
                if (inputField.text.ToLower() == "maddie.exe")
                {
                    gameWantsToLaunch = true;
                    am.CreateAchievement("StartGame");
                }

                else if (inputField.text.ToLower() == "help")
                {
                    screen = true;
                    textParent.SetActive(false);
                    hintObject.SetActive(true);
                }

                else if (inputField.text.ToLower() == "secret.txt")
                {
                    screen = true;
                    textParent.SetActive(false);
                    secretTxt.SetActive(true);
                }

                else if (inputField.text.ToLower() == "secret.bat")
                {
                    screen = true;
                    textParent.SetActive(false);
                    secret.SetActive(true);
                    am.CreateAchievement("DOS_Secret");
                }

                else
                {
                    screen = true;
                    textParent.SetActive(false);
                    badCommand.SetActive(true);
                }
            }

            else
            {
                screen = false;
                textParent.SetActive(true);
                hintObject.SetActive(false);
                badCommand.SetActive(false);
                secret.SetActive(false);
                secretTxt.SetActive(false);
            }

            if (!gameWantsToLaunch)
            {
                inputField.text = null;
            }
        }
        
        if (!gameWantsToLaunch)
        {
            inputField.Select();
        }

        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            timeToLaunchGame -= Time.deltaTime;
            text.SetActive(true);
        }

        if (timeToLaunchGame < 0f)
        {
            SceneManager.LoadScene("Logo");
        }
    }
}