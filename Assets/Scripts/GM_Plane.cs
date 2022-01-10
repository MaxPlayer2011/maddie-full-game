using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace GenericManagers
{
    public class GM_Plane : MonoBehaviour
    {
        private AudioSource audioSource;
        public AudioClip angry;
        public AudioClip crashClip;
        public AudioClip winClip;
        public float time;
        public float timeToCreateProblem;
        public float timeToDie;
        public float winEnd;
        public float crashClipTime;
        public string[] problems;
        private string[] possibleProblems = new string[5]
        {
        "Engine", "Fuel", "Steering", "Window", "Oxygen"
        };
        private int currentProblem = 0;
        public TextMeshProUGUI problemText;
        public int debug;
        public int points;
        public bool crashing;
        public bool win;
        public bool crashClipPlaying;
        public GameObject crash;
        public GameObject deathScreen;
        public GameObject hud;
        public GameObject yay;
        public Transform player;
        public Material skyCrash;
        public SpriteRenderer cloud;
        public Sprite cloudCrash;
        public Slider progress;
        public Animator anim;

        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            timeToCreateProblem = Random.Range(30, 60);
            debug = PlayerPrefs.GetInt("debug");
            crashClipTime = crashClip.length;
        }

        void Update()
        {
            progress.value = time;

            if (crashing == false)
            {
                if (timeToCreateProblem > 0f)
                {
                    timeToCreateProblem -= Time.deltaTime * 1f;
                }

                if (timeToCreateProblem < 0f & currentProblem == 0)
                {
                    timeToCreateProblem = Random.Range(30, 60);
                    problems[currentProblem] = possibleProblems[Random.Range(0, possibleProblems.Length)];
                    currentProblem += 1;
                }
            }

            if (time < 0 & win == false)
            {
                Win();
            }

            if (win == true)
            {
                winEnd -= Time.unscaledDeltaTime * 1f;
            }

            if (winEnd < 0)
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
                PlayerPrefs.SetInt("FT_Won", 1);
                GenericManagers.GUI.CursorManager.Unlock();
            }

            if (crashing == false)
            {
                time -= Time.deltaTime * 1f;
            }

            if (Input.GetKeyDown(KeyCode.E) & debug == 1 & !crashing)
            {
                Crashing();
            }

            if (time < 0f & win == false & crash == false)
            {
                Win();
            }

            if (timeToDie > 0 & crashing == true)
            {
                timeToDie -= Time.deltaTime * 1f;
            }

            if (timeToDie < 0 & crashClipPlaying == false)
            {
                crashClipPlaying = true;
                audioSource.PlayOneShot(crashClip);
                deathScreen.SetActive(true);
            }

            if (crashClipPlaying == true & crashClipTime > 0)
            {
                crashClipTime -= Time.deltaTime * 1f;
            }

            if (crashClipTime < 0 & crashClipPlaying == true)
            {
                SceneManager.LoadScene("BSOD02");
                PlayerPrefs.SetString("GameOver", "plane");
                PlayerPrefs.SetInt("planePoints", points);
            }

            problemText.text = "Problems:\n" + problems[0] + "\n" + problems[1] + "\n" + problems[2] + "\n" + problems[3] + "\n" + problems[4];
        }

        void Crashing()
        {
            crashing = true;
            crash.SetActive(true);
            anim.enabled = true;
            RenderSettings.skybox = skyCrash;
            cloud.sprite = cloudCrash;
            audioSource.PlayOneShot(angry, 2f);
        }

        public void FixProblem()
        {
            if (currentProblem > 0)
            {
                problems[currentProblem - 1] = null;
                currentProblem -= 1;
            }
        }

        void Win()
        {
            win = true;
            Time.timeScale = 0f;
            audioSource.volume = 0.3f;
            audioSource.pitch = 1f;
            audioSource.clip = winClip;
            audioSource.Play();
            hud.SetActive(false);
            yay.SetActive(true);
        }
    }
}
