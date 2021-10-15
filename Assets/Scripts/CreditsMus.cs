using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMus : MonoBehaviour
{
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Credits")
        {
            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                Destroy(gameObject);
            }
        }

        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (SceneManager.GetActiveScene().name != "Credits")
            {
                Destroy(gameObject);
            }
        }
    }
}