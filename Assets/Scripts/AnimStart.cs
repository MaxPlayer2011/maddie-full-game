using UnityEngine;

public class AnimStart : MonoBehaviour
{
    public string animName;
    
    void OnEnable()
    {
        GetComponent<Animator>().Play(animName);
    }
}
