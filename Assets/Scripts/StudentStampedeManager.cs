using UnityEngine;

namespace GenericManagers
{
    public class StudentStampedeManager : MonoBehaviour
    {
        public float timeToSpawn;
        public GameObject prefab;
        
        private void Start()
        {
            timeToSpawn = Random.Range(45, 90);
        }

        private void Update()
        {

        }
    }
}
