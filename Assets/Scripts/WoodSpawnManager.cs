using UnityEngine;

public class WoodSpawnManager : MonoBehaviour
{
    private float spawnTime = 30f;
    public int stickCount;
    public GameObject stick;

    private void Start()
    {
        SpawnSticks();
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
                SpawnSticks();
                spawnTime = Random.Range(30f, 50f);
            }
        }
    }

    private void SpawnSticks()
    {
        while (stickCount < 100)
        {
            float spawnX = Random.Range(-180, -20);
            float spawnZ = Random.Range(-80, 80);

            if (spawnX < -15 & spawnX > 15 & spawnZ < 15 & spawnZ > -85)
            {
                continue;
            }

            Instantiate(stick, new Vector3(spawnX, 5f, spawnZ), Quaternion.identity);
            stickCount++;
        }
    }
}