using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouseOverButton;
    private TextMeshProUGUI text;

    private void OnEnable()
    {
        mouseOverButton = false;
    }

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
            text.fontStyle = FontStyles.Underline;

        else
            if (!mouseOverButton)
                text.fontStyle = FontStyles.Normal;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOverButton = true;
        text.fontStyle = FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOverButton = false;
        text.fontStyle = FontStyles.Normal;
    }

    public void SetFontStyleToNormal()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
