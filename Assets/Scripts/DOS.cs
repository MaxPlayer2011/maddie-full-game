using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class DOS : MonoBehaviour
{
    private bool gameWantsToLaunch;
    private bool hint;
    private float timeToLaunchGame = 3f;
    public GameObject textParent;
    public GameObject hintObject;
    public GameObject badCommand;
    public GameObject text;
    public TMP_InputField inputField;
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!hint)
            {
                if (inputField.text == "MADDIE.EXE" | inputField.text == "maddie.exe")
                {
                    gameWantsToLaunch = true;
                }

                else if (inputField.text == "HELP" | inputField.text == "help")
                {
                    hint = true;
                    textParent.SetActive(false);
                    hintObject.SetActive(true);
                }

                else
                {
                    hint = true;
                    textParent.SetActive(false);
                    badCommand.SetActive(true);
                }
            }

            else
            {
                hint = false;
                textParent.SetActive(true);
                hintObject.SetActive(false);
                badCommand.SetActive(false);
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