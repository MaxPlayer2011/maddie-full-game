using System.Collections;
using UnityEngine;

public class Hear : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}