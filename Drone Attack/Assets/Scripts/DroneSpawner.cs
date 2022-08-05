using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    public float spawnDelay = 1f;

    float nextTimeToSpawn = 0f;

    public GameObject drone;

    public GameObject clone;

    public Transform[] spawnPoints;


    private void Update()
    {
        if (nextTimeToSpawn <= Time.time)
        {
            SpawnDrone();
            nextTimeToSpawn = Time.time + spawnDelay;
            DestroyGameObject();
            SpawnTimerIncrease();
        }
    }

    void SpawnDrone()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        clone = Instantiate(drone, spawnPoint.position, spawnPoint.rotation);
    }

    public void SpawnTimerIncrease()
    {
        if (spawnDelay > 0.3f)
        {
            spawnDelay -= 0.005f;
        }
        else
        {
            spawnDelay = 0.3f;
        }
    }

    public void DestroyGameObject()
    {
        Destroy(clone, 3);

    }


}

