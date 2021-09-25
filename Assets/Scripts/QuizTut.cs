using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizTut : MonoBehaviour
{
    public bool final;
    public int qNumber;
    public string correct;
    private GM_Tut gm;
    public TextMeshProUGUI title;
    public TMP_InputField answer;
    public GameObject next;
    public Image result;
    public Sprite correctSprite;
    public Sprite incorrectSprite;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM_Tut>();

        int num1 = Random.Range(0, 10);
        int num2 = Random.Range(0, 10);
        int plusOrMinus = Random.Range(0, 2);
        int Correct;

        if (plusOrMinus == 0)
        {
            Correct = num1 + num2;
            title.text = "Q" + qNumber + ": What is " + num1 + " + " + num2 + "?";
        }

        else
        {
            Correct = num1 - num2;
            title.text = "Q" + qNumber + ": What is " + num1 + " - " + num2 + "?";
        }

        correct = Correct + "";
    }

    void Update()
    {
        answer.Select();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (answer.text == correct)
            {
                result.sprite = correctSprite;
            }

            else
            {
                result.sprite = incorrectSprite;
                gm.incorrectAudio.Play();
            }

            if (final == true)
            {
                Time.timeScale = 1f;
                gm.UpdateScrapText();
                gm.StartCoroutine("tr3final");
                Destroy(transform.parent.gameObject);
            }

            else
            {
                gameObject.SetActive(false);
                next.SetActive(true);
            }
        }
    }
}