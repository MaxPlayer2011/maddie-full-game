using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("MainLevel");
            GameObject.FindGameObjectWithTag("ACH_Canvas").GetComponent<AchievementManager>().CreateAchievement("PassTut");
        }
    }
}