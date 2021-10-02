using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    public bool final;
    public bool spookyIntro;
    public int qNumber;
    public float timeToDark;
    private float timeToPlayMus = 0.40f;
    public string correct;
    private GameManager gm;
    private AudioSource audioSource;
    public TextMeshProUGUI title;
    public TMP_InputField answer;
    public GameObject next;
    public Image result;
    public Sprite correctSprite;
    public Sprite incorrectSprite;
    public GameObject spooky;
    public GameObject dark;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        audioSource = GetComponentInParent<AudioSource>();
        gm.welcome.Stop();

        if (!final)
        {
            int num1 = Random.Range(0, 10);
            int num2 = Random.Range(0, 10);
            int plusOrMinus = Random.Range(0, 2);

            if (plusOrMinus == 0)
            {
                correct = (num1 + num2).ToString();
                title.text = "Q" + qNumber + ": What is " + num1 + " + " + num2 + "?";
            }

            else
            {
                correct = (num1 - num2).ToString();
                title.text = "Q" + qNumber + ": What is " + num1 + " - " + num2 + "?";
            }
        }

        if (!gm.spooky)
        {
            gm.StopAudio();
        }
    }

    void Update()
    {
        answer.Select();

        if (timeToPlayMus > 0f & !gm.spooky)
        {
            timeToPlayMus -= Time.unscaledDeltaTime;
        }

        if (timeToPlayMus < 0f & !audioSource.isPlaying & !gm.spooky & !spookyIntro)
        {
            audioSource.Play();
        }

        if (timeToDark > 0f)
        {
            timeToDark -= Time.unscaledDeltaTime;
        }

        if (timeToDark < 0f)
        {
            dark.SetActive(true);
        }

        if (gm.spookyTime < 0f)
        {
            DestroyMathGame();
            gm.spookyTime = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (answer.text == correct)
            {
                result.sprite = correctSprite;
            }

            else
            {
                if (!spookyIntro)
                {
                    gm.maddieScript.anger += 1;
                }

                if (!gm.spooky)
                {
                    audioSource.Stop();
                    
                    if (!final)
                    {
                        gm.Spooky();
                    }

                    else
                    {
                        if (!spookyIntro)
                        {
                            spookyIntro = true;
                            gm.SpookyIntro();
                            timeToDark = 0.05f;
                            spooky.SetActive(true);
                            title.text = "Q4: <b>DIE</b>";
                        }
                    }
                }

                result.sprite = incorrectSprite;
            }

            if (final)
            {
                if (gm.spooky)
                {
                    DestroyMathGame();
                }

                if (gm.scraps == 9)
                {
                    gm.Finale();
                }
            }

            else
            {
                gameObject.SetActive(false);
                next.SetActive(true);
            }
        }
    }

    void DestroyMathGame()
    {
        Time.timeScale = 1f;
        gm.playerScript.stamina.value = 100f;
        gm.UpdateScapText();
        gm.learning = false;
        Destroy(transform.root.gameObject);
    }
}