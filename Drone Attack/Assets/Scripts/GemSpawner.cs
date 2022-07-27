using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public float spawnDelay = 3f;
    public float nextTimeToSpawn = 0f;
    public GameObject gem;
    public GameObject gemClone;
    public Transform[] gemSpawnPoints;

    private void Update()
    {
        if (nextTimeToSpawn <= Time.time)
        {
            SpawnGem();
            nextTimeToSpawn = Time.time + spawnDelay;
            DestroyGameObject();
        }
    }

    void SpawnGem()
    {
        int randomIndex = Random.Range(0, gemSpawnPoints.Length);
        Transform gemSpawnPoint = gemSpawnPoints[randomIndex];
        gemClone = Instantiate(gem, gemSpawnPoint.position, gemSpawnPoint.rotation);
    }

    public void DestroyGameObject()
    {
        Destroy(gemClone, 6);
    }
}

