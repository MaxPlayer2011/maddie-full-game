using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D normal;
    public Texture2D click;
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(click, Vector2.zero, CursorMode.Auto);
        }

        else
        {
            Cursor.SetCursor(normal, Vector2.zero, CursorMode.Auto);
        }
    }
}