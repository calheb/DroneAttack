using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    public float spawnDelay = 10f;
    public float nextTimeToSpawn = 0f;
    public GameObject heart;
    public GameObject heartClone;
    public Transform[] heartSpawnPoints;

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
        int randomIndex = Random.Range(0, heartSpawnPoints.Length);
        Transform heartSpawnPoint = heartSpawnPoints[randomIndex];
        heartClone = Instantiate(heart, heartSpawnPoint.position, heartSpawnPoint.rotation);
    }

    public void DestroyGameObject()
    {
        Destroy(heartClone, 6);
    }
}

