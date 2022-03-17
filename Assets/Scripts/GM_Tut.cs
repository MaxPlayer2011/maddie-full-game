using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GenericManagers.GUI;
using TMPro;

namespace GenericManagers
{
    public class GM_Tut : MonoBehaviour
    {
        private bool paused;
        private int neededScraps;
        public int scraps;
        private float timeToRespawn;
        public TextMeshProUGUI scrapText;
        public TextMeshProUGUI startText;
        public GameObject crosshair_hover;
        public GameObject pauseMenu;
        public GameObject printer;
        public GameObject maddie;
        public GameObject gameOverCamera;
        public GameObject trigger3;
        public GameObject trigger4;
        public GameObject trigger5;
        public Transform player;
        private CharacterController playerController;
        public Door lockedDoor;
        public ScrapTut scrap;
        public Transform[] playerTP;
        public Animator door;
        public AudioSource doorAudio;
        public AudioClip hgmTraGuideToNext;
        private string hgmTraGuideToNextText = "Please go to this green door.";
        public AudioClip hgmTraPass;
        private string hgmTraPassText = "Nice! You passed this training room.";
        private AudioSource audioSource;
        public AudioSource failAudio;
        public AudioSource incorrectAudio;
        public AudioClip[] failClips;
        public AudioClip DIE;
        public AudioQueueManager hgmAudioIntro;
        public AudioQueueManager hgmAudio1;
        public AudioQueueManager hgmAudio2;
        public AudioQueueManager hgmAudio3;
        public AudioQueueManager hgmAudio4;
        public AudioClip hgmIntro1;
        public AudioClip hgmIntro2;
        public AudioClip hgmIntro3;
        public AudioClip hgmIntro4;
        public AudioClip hgmTr1Intro1;
        public AudioClip hgmTr1Intro2;
        public AudioClip hgmTr1Explain1;
        public AudioClip hgmTr1Prepare1;
        public AudioClip hgmTr1Prepare2;
        public AudioClip hgmTr1GoodLuck;
        public AudioClip hgmTr2Intro1;
        public AudioClip hgmTr2Explain1;
        public AudioClip hgmTr2Explain2;
        public AudioClip hgmTr3Intro1;
        public AudioClip hgmTr3Explain1;
        public AudioClip hgmTr3Explain2;
        public AudioClip hgmTr3Prepare1;
        public AudioClip hgmTr4Intro1;
        public AudioClip hgmTr4Explain1;
        public AudioClip hgmTr4Explain2;
        public AudioClip hgmTr4Explain3;
        public AudioClip hgmTr4Explain4;
        public AudioClip hgmTr4Notice1;
        public AudioClip hgmTr4Praise1;
        public AudioClip hgmTr4guideToNext;
        public AudioClip hgmOutro1;

        void Start()
        {
            StartCoroutine("PrinterTalk");
            UpdateScrapText();
            playerController = player.GetComponent<CharacterController>();
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (Input.GetButtonDown("Pause"))
            {
                if (paused)
                {
                    Resume();
                }

                else
                {
                    Pause();
                }
            }

            if (Input.GetKeyDown(KeyCode.Q) & paused == true)
            {
                Time.timeScale = 1f;
                AudioListener.pause = false;
                SceneManager.LoadScene("MainMenu");
            }

            if (timeToRespawn > 0f)
            {
                timeToRespawn -= Time.unscaledDeltaTime;
            }

            if (timeToRespawn < 0f)
            {
                timeToRespawn = 0f;
                Time.timeScale = 1f;
                audioSource.Stop();
                audioSource.volume = 0.2f;
                audioSource.Play();
                gameOverCamera.SetActive(false);
                player.gameObject.SetActive(true);
                playerController.enabled = false;
                player.position = new Vector3(15f, 0f, -125f);
                player.localRotation = Quaternion.Euler(0f, 90f, 0f);
                playerController.enabled = true;
                maddie.transform.position = new Vector3(105f, 0f, -135f);
            }
        }

        IEnumerator PrinterTalk()
        {
            yield return new WaitForSeconds(3);
            startText.text = "Hello, my name is Help GameObject.";
            yield return new WaitForSeconds(3);
            startText.text = "I am here to guide you, and this tutorial will be as fast as a cheetah sprinting to <color=red>kill a <b>freaking, stupid cat! HAHAHA!</b></color>";
            yield return new WaitForSeconds(4);
            startText.text = "And in case you are wondering, I am the weird printer with the \"25¢\" sign.";
            yield return new WaitForSeconds(5);
            startText.text = "Also do you like dogs? I love them! They are nothing but cute little angels.";
            yield return new WaitForSeconds(6);
            startText.text = "But you know what kind of animal I hate?";
            yield return new WaitForSeconds(2);
            startText.text = "...";
            yield return new WaitForSeconds(1);
            startText.text = "Cats!";
            yield return new WaitForSeconds(0.5f);
            startText.text = "All they do is scratch, be annoying, be annoying, be annoying and be annoying!!!";
            yield return new WaitForSeconds(5);
            startText.text = "Also I didn't tell anything \"tutory\".";
            yield return new WaitForSeconds(2);
            startText.text = "HAHAHAAHAHAHAHAHAAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHA";
            yield return new WaitForSeconds(5);
            printer.SetActive(false);
            startText.text = null;
            door.Play("Open");
            doorAudio.Play();
        }

        public void UpdateScrapText()
        {
            scrapText.text = scraps + "/" + neededScraps + " Poem Scraps";
        }

        public void TPPlayer(int number)
        {
            playerController.enabled = false;
            player.position = playerTP[number].position;
            player.localRotation = Quaternion.Euler(0f, 90f, 0f);
            playerController.enabled = true;
        }

        public void GameOver()
        {
            AudioClip randomClip = failClips[Random.Range(0, failClips.Length)];

            gameOverCamera.SetActive(true);
            player.gameObject.SetActive(false);
            timeToRespawn = randomClip.length;
            failAudio.PlayOneShot(randomClip);
            audioSource.Stop();
            audioSource.volume = 0.3f;
            audioSource.PlayOneShot(DIE, 0.8f);
            Time.timeScale = 0f;
        }

        void Pause()
        {
            paused = true;
            Time.timeScale = 0f;
            AudioListener.pause = true;
            pauseMenu.SetActive(true);
            CursorManager.Unlock();
        }

        public void Resume()
        {
            paused = false;
            Time.timeScale = 1f;
            AudioListener.pause = false;
            pauseMenu.SetActive(false);
            CursorManager.Lock();
        }

        public IEnumerator Intro()
        {
            hgmAudioIntro.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            hgmAudioIntro.Queue(hgmIntro1, "Hello, I am the real Help GameObject.");
            hgmAudioIntro.Queue(hgmIntro2, "That printer lied to you, because he is an asshole.");
            hgmAudioIntro.Queue(hgmIntro3, "I am going to guide you through the tutorial.");
            hgmAudioIntro.Queue(hgmIntro4, "Teleporting GameObject \"Player\" in 3... 2... 1.");
            yield return new WaitForSeconds(hgmIntro1.length + hgmIntro2.length + hgmIntro3.length + hgmIntro4.length);
            TPPlayer(0);
            StartCoroutine("tr1");
        }

        IEnumerator tr1()
        {
            hgmAudio1.Queue(hgmTr1Intro1, "First, we will learn about Maddie.");
            hgmAudio1.Queue(hgmTr1Intro2, "He is a big asshole.");
            hgmAudio1.Queue(hgmTr1Explain1, "He will try to kill you with a chainsaw.");
            hgmAudio1.Queue(hgmTr1Prepare1, "So let's train you to bypass him.");
            hgmAudio1.Queue(hgmTr1Prepare2, "Releasing Maddie in 3... 2... 1.");
            hgmAudio1.Queue(hgmTr1GoodLuck, "Good luck.");
            yield return new WaitForSeconds(hgmTr1Intro1.length + hgmTr1Intro2.length + hgmTr1Explain1.length + hgmTr1Prepare1.length + hgmTr1Prepare2.length + hgmTr1GoodLuck.length);
            hgmAudio1.gameObject.SetActive(false);
            hgmAudio1.gameObject.GetComponent<Subtitle>().subtitle.gameObject.SetActive(false);
            lockedDoor.locked = false;
            maddie.SetActive(true);
        }

        public IEnumerator tr2()
        {
            maddie.SetActive(false);
            hgmAudio2.Queue(hgmTraPass, hgmTraPassText);
            hgmAudio2.Queue(hgmTr2Intro1, "Alright! The next training room is about the pause menu.");
            hgmAudio2.Queue(hgmTr2Explain1, "To pause, click \"ESC\" or \"P\" on your keyboard.");
            hgmAudio2.Queue(hgmTr2Explain2, "Click the same buttons to resume, or click the \"Resume\" button on the screen.");
            hgmAudio2.Queue(hgmTraPass, hgmTraPassText);
            hgmAudio2.Queue(hgmTraGuideToNext, hgmTraGuideToNextText);
            yield return new WaitForSeconds(hgmTraPass.length + hgmTr2Intro1.length + hgmTr2Explain1.length + hgmTr2Explain2.length + hgmTraPass.length + hgmTraGuideToNext.length);
            trigger3.SetActive(true);
        }

        public IEnumerator tr3()
        {
            TPPlayer(1);
            hgmAudio3.Queue(hgmTr3Intro1, "Alright! T.R. #3: Poem scraps.");
            hgmAudio3.Queue(hgmTr3Explain1, "Left-click on the poem scrap, or click \"E\" on it.");
            hgmAudio3.Queue(hgmTr3Explain2, "Then, Maddie will give you some math problems.");
            hgmAudio3.Queue(hgmTr3Prepare1, "Go on! Try it out!");
            yield return new WaitForSeconds(hgmTr3Intro1.length + hgmTr3Explain1.length + hgmTr3Explain2.length + hgmTr3Prepare1.length);
            neededScraps = 1;
            UpdateScrapText();
            scrap.enabled = true;
        }

        public IEnumerator tr3final()
        {
            hgmAudio3.Queue(hgmTraPass, hgmTraPassText);
            hgmAudio3.Queue(hgmTraGuideToNext, hgmTraGuideToNextText);
            yield return new WaitForSeconds(hgmTraPass.length + hgmTraGuideToNext.length);
            trigger4.SetActive(true);
        }

        public IEnumerator tr4()
        {
            TPPlayer(2);
            hgmAudio4.Queue(hgmTr4Intro1, "Alright! Last training room: characters.");
            hgmAudio4.Queue(hgmTr4Explain1, "Principal... the bald guy in the gray shirt... will give you detention if you break the rules.");
            hgmAudio4.Queue(hgmTr4Explain2, "Friday Night Yeah!... the creepy piece of paper... will force you to listen to his crappy music for 5 seconds.");
            hgmAudio4.Queue(hgmTr4Explain3, "Bully, the idiot behind you, will try to steal one of your items.");
            hgmAudio4.Queue(hgmTr4Explain4, "Make sure to read the info about using items on that right wall.");
            hgmAudio4.Queue(hgmTr4Notice1, "Other characters coming soon!");
            hgmAudio4.Queue(hgmTr4Praise1, "Alright! You are ready to play the game!");
            hgmAudio4.Queue(hgmTr4guideToNext, "Please go to this green door to play the game.");
            hgmAudio4.Queue(hgmOutro1, "Bye! Good luck!");
            yield return new WaitForSeconds(hgmTr4Intro1.length + hgmTr4Explain1.length + hgmTr4Explain2.length + hgmTr4Explain3.length + hgmTr4Explain4.length + hgmTr4Notice1.length + hgmTr4Praise1.length + hgmTr4guideToNext.length + hgmOutro1.length);
            trigger5.SetActive(true);
        }
    }
}
