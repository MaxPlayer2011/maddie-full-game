using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GenericManagers.GUI;
using TMPro;

namespace GenericManagers
{
    public class GameManager : MonoBehaviour
    {
        public bool spooky;
        public bool gameOver;
        public bool scrapJoining;
        public bool allScrapsJoined;
        public bool detention;
        public bool learning;
        public bool Event;
        public bool waterEvent;
        public bool fireEvent;
        public bool secretEnding;
        public float spookyTime;
        public float detentionTime;
        public float rapTime;
        public float gameOverEnd;
        public float timeToStartEvent;
        public float timeToStopEvent;
        public int scraps;
        public int exitsReached;
        public int[] items;
        public int slotSelected;
        public string[] itemNames;
        public string dialoge_null;
        public TextMeshProUGUI itemText;
        public Image[] itemImages;
        public Sprite[] itemTextures;
        public GameObject[] selectArrows;
        private AudioSource audioSource;
        public AudioSource scareAudio;
        public AudioSource preScareAudio;
        public GameObject finaleAudio;
        public AudioClip MUS_default;
        public AudioClip MUS_spooky;
        public AudioClip ANGRY;
        public AudioClip LIGHTS_TURN_OFF;
        public AudioClip[] nineScraps;
        public AudioClip exitReached;
        public AudioClip finaleIntro;
        public AudioClip LOUD;
        public AudioClip[] deathClips;
        private AudioClip currentDeathClip;
        private AudioQueueManager maddieAudioQueue;
        public Transform player;
        [HideInInspector]
        public Player playerScript;
        [HideInInspector]
        public CharacterController playerController;
        public Subtitle finaleAudioSubtitles;
        public GameObject flashlight;
        public Door detentionDoor;
        public Camera gameOverCamera;
        public GameObject math;
        public GameObject soda;
        public GameObject entrances;
        public GameObject enemyNPC;
        public Maddie maddieScript;
        public PRI priScript;
        public Rap rapScript;
        public Bully bullyScript;
        public AudioSource printer;
        public GameObject startBarriers;
        public GameObject bus;
        public Transform[] aiWanderPoints;
        public Transform[] bullySpawnPoints;
        private AudioSource rapMusic;
        public AudioSource welcome;
        public GameObject pauseMenu;
        public TextMeshProUGUI restText;
        public GameObject crosshair_hover;
        public TextMeshProUGUI scrapCount;
        public TextMeshProUGUI detentionText;
        public TextMeshProUGUI rapText;
        public Animator horrorBird_anim;
        public GameObject secretWall;
        public PauseManager pm;
        public Color scaryLight;
        public Color finalFogColor;
        public GameObject hud;
        public GameObject water;
        public GameObject scrapConnect;
        public GameObject scrapFull;
        public GameObject[] scrapButton;
        public Slider mouseSensitivity;
        public Toggle subtitleToggle;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            playerScript = player.gameObject.GetComponent<Player>();
            playerController = player.gameObject.GetComponent<CharacterController>();
            rapMusic = rapScript.gameObject.GetComponent<AudioSource>();
            audioSource.clip = MUS_default;
            audioSource.loop = true;
            audioSource.volume = 0.5f;
            audioSource.Play();
            currentDeathClip = deathClips[Random.Range(0, deathClips.Length)];
            maddieAudioQueue = finaleAudio.AddComponent<AudioQueueManager>();
            mouseSensitivity.value = PlayerPrefs.GetFloat("mouseSensitivity");

            if (currentDeathClip != deathClips[0])
            {
                gameOverEnd = currentDeathClip.length;
            }

            else
            {
                gameOverEnd = 1.967f;
            }

            if (PlayerPrefs.GetInt("subtitle") == 1)
            {
                subtitleToggle.isOn = true;
            }

            else
            {
                subtitleToggle.isOn = false;
            }

            if (mouseSensitivity.value == 1f)
            {
                mouseSensitivity.value = 3f;
            }

            UpdateScapText();
            UpdateInventoryData();
        }

        void Update()
        {
            PlayerPrefs.SetFloat("mouseSensitivity", mouseSensitivity.value);

            if (subtitleToggle.isOn)
            {
                PlayerPrefs.SetInt("subtitle", 1);
            }

            else
            {
                PlayerPrefs.SetInt("subtitle", 0);
            }

            if (spookyTime > 0f)
            {
                spookyTime -= Time.unscaledDeltaTime;
            }

            if (spookyTime < 0f & !spooky)
            {
                Spooky();
            }

            if (!gameOver & !learning & !scrapJoining & !playerScript.squished)
            {
                pm.enabled = true;
            }

            else
            {
                pm.enabled = false;
            }

            if (scraps == 4)
            {
                secretWall.SetActive(false);
            }

            else
            {
                secretWall.SetActive(true);
            }

            if (gameOver)
            {
                gameOverEnd -= Time.unscaledDeltaTime * 1f;
                gameOverCamera.farClipPlane = gameOverEnd * 100f;
            }

            if (gameOverEnd < 0)
            {
                GameOverEnd("normal");
            }

            if (detentionTime > 0f)
            {
                detentionTime -= Time.deltaTime * 1;
                detentionText.text = "Detention in process,\n" + Mathf.CeilToInt(detentionTime) + " seconds remain.";
            }

            else
            {
                detention = false;
                detentionText.text = null;
                detentionDoor.locked = false;
            }

            if (detention)
            {
                playerScript.detention = true;
            }

            else
            {
                playerScript.detention = false;
            }

            if (playerScript.guilt == "escape" & !detention & !priScript.angry)
            {
                playerScript.guilty = false;
                playerScript.guilt = null;
            }

            if (rapTime > 0f & !gameOver)
            {
                rapTime -= Time.deltaTime;
                rapText.text = "Listen to this beautiful music!\n(" + Mathf.CeilToInt(rapTime) + ")";
            }

            else
            {
                playerScript.rap = false;
                rapText.text = null;
                if (rapScript.gameObject.activeInHierarchy)
                {
                    if (rapScript.agent.isStopped)
                    {
                        rapScript.agent.isStopped = false;
                    }
                }
            }

            if (spooky)
            {
                if (!Event & timeToStartEvent > 0f)
                {
                    timeToStartEvent -= Time.deltaTime * 1f;
                }

                if (!Event & timeToStartEvent < 0f)
                {
                    StartEvent(Random.Range(0, 0));
                }

                if (Event & timeToStopEvent > 0f)
                {
                    timeToStopEvent -= Time.deltaTime * 1f;
                }

                if (Event & timeToStopEvent < 0f)
                {
                    Event = false;
                    waterEvent = false;
                    water.SetActive(false);
                    fireEvent = false;
                    timeToStartEvent = Random.Range(560, 620);
                }
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetButtonDown("Interact"))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.name == "SecretCube" & Vector3.Distance(player.position, hit.transform.position) < 10f)
                    {
                        secretEnding = true;
                        Destroy(hit.transform.gameObject);
                    }
                }
            }

            if (maddieScript.antiHearingTime < 0f & maddieScript.antiHearing)
            {
                maddieScript.antiHearing = false;
                printer.Stop();
            }

            if (Input.GetKeyDown(KeyCode.Return) & allScrapsJoined)
            {
                if (!secretEnding)
                {
                    SceneManager.LoadScene("Won");
                }

                else
                {
                    SceneManager.LoadScene("Secret");
                }

                Time.timeScale = 1f;
                CursorManager.Lock();
            }

            bool allScrapButtonsAreFalse = true;

            foreach (var button in scrapButton)
            {
                if (button.activeSelf)
                {
                    allScrapButtonsAreFalse = false;
                    break;
                }
            }

            if (allScrapButtonsAreFalse)
            {
                allScrapsJoined = true;
                scrapFull.SetActive(true);
            }

            if (Input.GetButtonDown("Use Item"))
            {
                UseItem();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                slotSelected = 0;
                UpdateInventoryData();
            }

            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                slotSelected = 1;
                UpdateInventoryData();
            }

            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                slotSelected = 2;
                UpdateInventoryData();
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                slotSelected += 1;

                if (slotSelected > 2)
                {
                    slotSelected = 0;
                }

                UpdateInventoryData();
            }

            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                slotSelected -= 1;

                if (slotSelected < 0)
                {
                    slotSelected = 2;
                }

                UpdateInventoryData();
            }

            if (slotSelected == 0)
            {
                selectArrows[0].SetActive(true);
                selectArrows[1].SetActive(false);
                selectArrows[2].SetActive(false);
            }

            else if (slotSelected == 1)
            {
                selectArrows[0].SetActive(false);
                selectArrows[1].SetActive(true);
                selectArrows[2].SetActive(false);
            }

            else if (slotSelected == 2)
            {
                selectArrows[0].SetActive(false);
                selectArrows[1].SetActive(false);
                selectArrows[2].SetActive(true);
            }
        }

        public void Spooky()
        {
            spooky = true;
            playerScript.spooky = true;
            welcome.gameObject.SetActive(false);
            enemyNPC.SetActive(true);
            startBarriers.SetActive(false);
            audioSource.clip = MUS_spooky;
            audioSource.volume = 0.3f;
            audioSource.Play();
            scareAudio.PlayOneShot(ANGRY, 0.8f);
            RenderSettings.fog = enabled;
            RenderSettings.ambientLight = scaryLight;
            RenderSettings.skybox = null;
            flashlight.SetActive(true);
            horrorBird_anim.enabled = true;
            bus.SetActive(false);
            scrapCount.color = Color.white;
            restText.color = Color.white;
        }

        public void SpookyIntro()
        {
            audioSource.Stop();
            spookyTime = LIGHTS_TURN_OFF.length;
            preScareAudio.transform.position = playerScript.Camera.transform.position + Vector3.forward * 5f;
            player.localRotation = Quaternion.identity;
            preScareAudio.PlayOneShot(LIGHTS_TURN_OFF, 0.8f);
        }

        public void Finale()
        {
            maddieAudioQueue.Queue(nineScraps[0], "CONGRATULATIONS!");
            maddieAudioQueue.Queue(nineScraps[1], "YOU HAVE SOLVED ALL OF MY MATH PROBLEMS!");
            maddieAudioQueue.Queue(nineScraps[2], "TRY, IF YOU CAN, IF YOU STILL CAN, TO...");
            maddieAudioQueue.Queue(nineScraps[3], "<b>GET YOUR STUPID ASS OUT OF THIS STUPID SCHOOL! JUST... UGHHH! GOD!</b>");
            entrances.transform.position = new Vector3(0f, 10f, 0f);
        }

        private IEnumerator FinaleIntro()
        {
            audioSource.Stop();
            yield return new WaitForSeconds(finaleIntro.length);
            audioSource.clip = finaleIntro;
            audioSource.volume = 1f;
            audioSource.Play();
        }

        public void ExitReached()
        {
            exitsReached += 1;

            if (exitsReached == 1)
            {
                RenderSettings.ambientLight = Color.red;
                RenderSettings.fogColor = finalFogColor;
                flashlight.SetActive(false);
                StartCoroutine(FinaleIntro());
            }

            else if (exitsReached == 2)
            {
                audioSource.clip = LOUD;
                audioSource.Play();
            }

            audioSource.PlayOneShot(exitReached);
        }

        public void TeleportPlayer(Vector3 position, Quaternion rotation)
        {
            playerController.enabled = false;
            player.position = position;
            player.localRotation = rotation;
            playerController.enabled = true;
        }

        public void ScrapJoin()
        {
            Time.timeScale = 0f;
            hud.SetActive(false);
            scrapConnect.SetActive(true);
            CursorManager.Unlock();
            scrapJoining = true;
        }

        public void GameOver()
        {
            gameOver = true;
            Time.timeScale = 0f;
            player.gameObject.SetActive(false);
            gameOverCamera.gameObject.SetActive(true);
            maddieScript.enabled = false;
            audioSource.Stop();
            rapMusic.Stop();
            audioSource.PlayOneShot(currentDeathClip, 7f);
        }

        public void GameOverEnd(string type)
        {
            SceneManager.LoadScene("BSOD02");
            PlayerPrefs.SetString("GameOver", type);
            Time.timeScale = 1f;
        }

        private void StartEvent(int id)
        {
            Event = true;
            timeToStopEvent = Random.Range(30, 45);

            switch (id)
            {
                case 0:
                    waterEvent = true;
                    water.SetActive(true);
                    break;
            }
        }

        public void Menu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
            AudioListener.pause = false;
            AudioListener.volume = 1f;
            CursorManager.Unlock();
        }

        public void ButtonEnter(TextMeshProUGUI text)
        {
            text.fontStyle = FontStyles.Underline;
        }

        public void ButtonExit(TextMeshProUGUI text)
        {
            text.fontStyle = FontStyles.Normal;
        }

        public void UpdateScapText()
        {
            scrapCount.text = scraps + "/9 Poem Scraps";
        }

        public void UpdateInventoryData()
        {
            itemImages[0].sprite = itemTextures[items[0]];
            itemImages[1].sprite = itemTextures[items[1]];
            itemImages[2].sprite = itemTextures[items[2]];
            itemText.text = itemNames[items[slotSelected]];
        }

        public void AddItem(int id)
        {
            if (items[0] == 0)
            {
                items[0] = id;
            }

            else if (items[1] == 0)
            {
                items[1] = id;
            }

            else if (items[2] == 0)
            {
                items[2] = id;
            }

            else
            {
                items[slotSelected] = id;
            }

            UpdateInventoryData();
        }

        private void UseItem()
        {
            if (items[slotSelected] != 0)
            {
                switch (items[slotSelected])
                {
                    case 1:

                        Instantiate(soda, new Vector3(player.position.x, 5, player.position.z), player.localRotation);
                        RemoveItem();
                        break;

                    case 2:
                        playerScript.energetic = true;
                        playerScript.energyTime = 20f;
                        playerScript.stamina.value = 100f;
                        RemoveItem();
                        break;

                    case 3:
                        if (playerScript.rap)
                        {
                            rapScript.Cut();
                            RemoveItem();
                        }
                        break;

                    case 4:
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray, out hit) & Vector3.Distance(player.position, priScript.transform.position) <= 20f)
                        {
                            if (hit.transform.name == "PRI" & !priScript.busy & !detention)
                            {
                                priScript.Distract();
                                RemoveItem();
                            }
                        }

                        break;

                    case 5:
                        RaycastHit hit2;
                        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray2, out hit2) & Vector3.Distance(player.position, maddieScript.transform.position) <= 20f)
                        {
                            if (hit2.transform.name == "Maddie" & !maddieScript.preparing & !maddieScript.cutting & !maddieScript.antiHearing)
                            {
                                maddieScript.CutLog();
                                RemoveItem();

                            }
                        }
                        break;

                    case 6:
                        RaycastHit hit3;
                        Ray ray3 = Camera.main.ScreenPointToRay(Input.mousePosition);

                        if (Physics.Raycast(ray3, out hit3))
                        {
                            if (hit3.transform.name == "PrinterButton" & Vector3.Distance(player.position, hit3.transform.position) < 10f & !maddieScript.antiHearing)
                            {
                                printer.Play();
                                maddieScript.antiHearing = true;
                                maddieScript.antiHearingTime = 30f;
                                RemoveItem();
                            }
                        }
                        break;
                }
            }
        }

        private void RemoveItem()
        {
            items[slotSelected] = 0;
            UpdateInventoryData();
        }

        public void StopAudio()
        {
            audioSource.Stop();
        }
    }
}
