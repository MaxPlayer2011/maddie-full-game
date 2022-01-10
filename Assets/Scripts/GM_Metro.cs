using UnityEngine;

namespace GenericManagers
{
    public class GM_Metro : MonoBehaviour
    {
        private Vector3 startPosition = new Vector3(-11f, 4f, -300f);
        private Vector3 platformPosition = new Vector3(-11f, 4f, 5f);
        private int state = 1;
        private float timeToMove = 5f;
        private float z = 0f;
        private float zShift = 1f;

        void Start()
        {
            transform.position = startPosition;
        }

        void Update()
        {
            switch (state)
            {
                case 1:
                    z = Mathf.Lerp(transform.position.z, platformPosition.z, 0.01f);
                    transform.position = new Vector3(transform.position.x, transform.position.y, z);

                    if (transform.position.z >= 4.99f)
                    {
                        state = 2;
                    }
                    break;
                case 2:
                    timeToMove -= Time.deltaTime;

                    if (timeToMove < 0f)
                    {
                        state = 3;
                    }
                    break;
                case 3:
                    z = Mathf.Lerp(transform.position.z, transform.position.z + zShift, 0.01f);
                    zShift += 2 * Time.deltaTime;

                    transform.position = new Vector3(transform.position.x, transform.position.y, z);

                    if (transform.position.z >= 300f)
                    {
                        state = 1;
                        transform.position = startPosition;
                        zShift = 1f;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
