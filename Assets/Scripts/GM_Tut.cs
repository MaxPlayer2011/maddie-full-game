using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public AudioSource hgmAudioIntro;
    public Subtitle dialogeIntro;
    public AudioSource hgmAudio1;
    public Subtitle dialoge1;
    public AudioSource hgmAudio2;
    public Subtitle dialoge2;
    public AudioSource hgmAudio3;
    public Subtitle dialoge3;
    public AudioSource hgmAudio4;
    public Subtitle dialoge4;
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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        paused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public IEnumerator Intro()
    {
        hgmAudioIntro.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        hgmAudioIntro.PlayOneShot(hgmIntro1);
        dialogeIntro.text.text = "Hello, I am the real Help GameObject.";
        yield return new WaitForSeconds(hgmIntro1.length);
        hgmAudioIntro.PlayOneShot(hgmIntro2);
        dialogeIntro.text.text = "That printer lied to you, because he is an asshole.";
        yield return new WaitForSeconds(hgmIntro2.length);
        hgmAudioIntro.PlayOneShot(hgmIntro3);
        dialogeIntro.text.text = "I am going to guide you through the tutorial.";
        yield return new WaitForSeconds(hgmIntro3.length);
        hgmAudioIntro.PlayOneShot(hgmIntro4);
        dialogeIntro.text.text = "Teleporting GameObject \"Player\" in 3... 2... 1.";
        yield return new WaitForSeconds(hgmIntro4.length);
        TPPlayer(0);
        StartCoroutine("tr1");
    }

    IEnumerator tr1()
    {
        hgmAudio1.PlayOneShot(hgmTr1Intro1);
        dialoge1.text.text = "First, we will learn about Maddie.";
        yield return new WaitForSeconds(hgmTr1Intro1.length);
        hgmAudio1.PlayOneShot(hgmTr1Intro2);
        dialoge1.text.text = "He is a big asshole.";
        yield return new WaitForSeconds(hgmTr1Intro2.length);
        hgmAudio1.PlayOneShot(hgmTr1Explain1);
        dialoge1.text.text = "He will try to kill you with a chainsaw.";
        yield return new WaitForSeconds(hgmTr1Explain1.length);
        hgmAudio1.PlayOneShot(hgmTr1Prepare1);
        dialoge1.text.text = "So let's train you to bypass him.";
        yield return new WaitForSeconds(hgmTr1Prepare1.length);
        hgmAudio1.PlayOneShot(hgmTr1Prepare2);
        dialoge1.text.text = "Releasing Maddie in 3... 2... 1.";
        yield return new WaitForSeconds(hgmTr1Prepare2.length);
        hgmAudio1.PlayOneShot(hgmTr1GoodLuck);
        dialoge1.text.text = "Good luck.";
        yield return new WaitForSeconds(hgmTr1GoodLuck.length);
        hgmAudio1.gameObject.SetActive(false);
        dialoge1.subtitle.gameObject.SetActive(false);
        lockedDoor.locked = false;
        maddie.SetActive(true);
    }

    public IEnumerator tr2()
    {
        maddie.SetActive(false);
        hgmAudio2.PlayOneShot(hgmTraPass);
        dialoge2.text.text = hgmTraPassText;
        yield return new WaitForSeconds(hgmTraPass.length);
        hgmAudio2.PlayOneShot(hgmTr2Intro1);
        dialoge2.text.text = "Alright! The next training room is about the pause menu.";
        yield return new WaitForSeconds(hgmTr2Intro1.length);
        hgmAudio2.PlayOneShot(hgmTr2Explain1);
        dialoge2.text.text = "To pause, click \"ESC\" or \"P\" on your keyboard.";
        yield return new WaitForSeconds(hgmTr2Explain1.length);
        hgmAudio2.PlayOneShot(hgmTr2Explain2);
        dialoge2.text.text = "Click the same buttons to resume, or click the \"Resume\" button on the screen.";
        yield return new WaitForSeconds(hgmTr2Explain2.length);
        hgmAudio2.PlayOneShot(hgmTraPass);
        dialoge2.text.text = hgmTraPassText;
        yield return new WaitForSeconds(hgmTraPass.length);
        hgmAudio2.PlayOneShot(hgmTraGuideToNext);
        dialoge2.text.text = hgmTraGuideToNextText;
        yield return new WaitForSeconds(hgmTraGuideToNext.length);
        trigger3.SetActive(true);
    }

    public IEnumerator tr3()
    {
        TPPlayer(1);
        hgmAudio3.PlayOneShot(hgmTr3Intro1);
        dialoge3.text.text = "Alright! T.R. #3: Poem scraps.";
        yield return new WaitForSeconds(hgmTr3Intro1.length);
        hgmAudio3.PlayOneShot(hgmTr3Explain1);
        dialoge3.text.text = "Left-click on the poem scrap, or click \"E\" on it.";
        yield return new WaitForSeconds(hgmTr3Explain1.length);
        hgmAudio3.PlayOneShot(hgmTr3Explain2);
        dialoge3.text.text = "Then, Maddie will give you some math problems.";
        yield return new WaitForSeconds(hgmTr3Explain2.length);
        hgmAudio3.PlayOneShot(hgmTr3Prepare1);
        dialoge3.text.text = "Go on! Try it out!";
        yield return new WaitForSeconds(hgmTr3Prepare1.length);
        neededScraps = 1;
        UpdateScrapText();
        scrap.enabled = true;
    }

    public IEnumerator tr3final()
    {
        hgmAudio3.PlayOneShot(hgmTraPass);
        dialoge3.text.text = hgmTraPassText;
        yield return new WaitForSeconds(hgmTraPass.length);
        hgmAudio3.PlayOneShot(hgmTraGuideToNext);
        dialoge3.text.text = hgmTraGuideToNextText;
        yield return new WaitForSeconds(hgmTraGuideToNext.length);
        trigger4.SetActive(true);
    }

    public IEnumerator tr4()
    {
        TPPlayer(2);
        hgmAudio4.PlayOneShot(hgmTr4Intro1);
        dialoge4.text.text = "Alright! Last training room: characters.";
        yield return new WaitForSeconds(hgmTr4Intro1.length);
        hgmAudio4.PlayOneShot(hgmTr4Explain1);
        dialoge4.text.text = "Principal... the bald guy in the gray shirt... will give you detention if you break the rules. Make sure to look at the rule poster when you start playing.";
        yield return new WaitForSeconds(hgmTr4Explain1.length);
        hgmAudio4.PlayOneShot(hgmTr4Explain2);
        dialoge4.text.text = "Friday Night Yeah!... the creepy peace of paper... will force you to listen to his crappy music for 5 seconds.";
        yield return new WaitForSeconds(hgmTr4Explain2.length);
        hgmAudio4.PlayOneShot(hgmTr4Explain3);
        dialoge4.text.text = "Bully, the idiot behind you, will try to steal one of your items.";
        yield return new WaitForSeconds(hgmTr4Explain3.length);
        hgmAudio4.PlayOneShot(hgmTr4Notice1);
        dialoge4.text.text = "Other characters coming soon!";
        yield return new WaitForSeconds(hgmTr4Notice1.length);
        hgmAudio4.PlayOneShot(hgmTr4Praise1);
        dialoge4.text.text = "Alright! You are ready to play the game!";
        yield return new WaitForSeconds(hgmTr4Praise1.length);
        hgmAudio4.PlayOneShot(hgmTr4guideToNext);
        dialoge4.text.text = "Please go to this green door to play the game.";
        yield return new WaitForSeconds(hgmTr4guideToNext.length);
        hgmAudio4.PlayOneShot(hgmOutro1);
        dialoge4.text.text = "Bye! Good luck!";
        yield return new WaitForSeconds(hgmOutro1.length);
        trigger5.SetActive(true);
    }
}