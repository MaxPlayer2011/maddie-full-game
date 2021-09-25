using UnityEngine;
using UnityEngine.UI;

public class ScrapJoinButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GetScrap);
    }

    public void GetScrap()
    {
        gameObject.SetActive(false);
    }
}