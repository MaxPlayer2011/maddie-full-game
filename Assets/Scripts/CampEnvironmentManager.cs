using UnityEngine;

public class CampEnvironmentManager : MonoBehaviour
{
    private float spawnTime = 30f;
    public int stickCount;
    public GameObject tree;
    public GameObject stick;

    private void Start()
    {
        Spawn(tree);
        Spawn(stick);
    }

    private void Update()
    {
        if (stickCount < 100)
        {
            if (spawnTime > 0f)
            {
                spawnTime -= Time.deltaTime;
            }

            if (spawnTime < 0f)
            {
                Spawn(stick);
                spawnTime = Random.Range(30f, 50f);
            }
        }
    }

    private void Spawn(GameObject Object)
    {
        while (stickCount < 100)
        {
            float spawnX = Random.Range(-180, -20);
            float spawnZ = Random.Range(-80, 80);

            if (spawnX < -15 & spawnX > 15 & spawnZ < 15 & spawnZ > -85)
            {
                continue;
            }

            Instantiate(Object, new Vector3(spawnX, 5f, spawnZ), Quaternion.identity);
            stickCount++;
        }
    }
}