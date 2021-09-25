using UnityEngine;

public class Water : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        meshRenderer.material.SetTextureOffset("_MainTex", new Vector2(0.1f, 0.1f) * Time.time);
    }
}