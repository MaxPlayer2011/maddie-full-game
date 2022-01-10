using UnityEngine;
using TMPro;

public class MacTextFix : MonoBehaviour
{
    public TMP_FontAsset font;
    
    private void Start()
    {
        if (Application.platform != RuntimePlatform.OSXPlayer)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        var textComponents = Component.FindObjectsOfType<TextMeshProUGUI>();

        foreach (var component in textComponents)
        {
            if (component.font.atlasRenderMode == UnityEngine.TextCore.LowLevel.GlyphRenderMode.SDF)
            {
                component.font = font;
            }
        }
    }
}
