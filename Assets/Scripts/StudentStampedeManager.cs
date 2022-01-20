using UnityEngine;

namespace GenericManagers
{
    public class StudentStampedeManager : MonoBehaviour
    {
        public float timeToSpawn;
        private AudioSource audioSource;
        public Transform[] spawnPoints;
        public GameObject stampede;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().spooky)
                timeToSpawn -= Time.deltaTime;

            if (timeToSpawn < 0f)
            {
                timeToSpawn = Random.Range(120, 180);

                int spawnPoint = Random.Range(0, spawnPoints.Length);
                Instantiate(stampede, new Vector3(spawnPoints[spawnPoint].position.x, 5, spawnPoints[spawnPoint].position.z), spawnPoints[spawnPoint].localRotation);
            }

            if (timeToSpawn <= audioSource.clip.length & !audioSource.isPlaying)
                audioSource.Play();
        }
    }
}
