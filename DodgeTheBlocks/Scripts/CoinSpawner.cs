using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class CoinSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    //public GameObject coinPrefab;
    public Transform coinPrefab;
    private float timeToSpawn = 0.5f;
    private float timeBetweenWaves = 2f;
    private int cointimeMultiplier = 1;

    void Update()
    {
        if (Time.time >= timeToSpawn)
        {
            SpawnBlocks();
            timeToSpawn = Time.time + timeBetweenWaves;
        }
        /*
        if (Time.time > 60 * cointimeMultiplier)
        {
            timeBetweenWaves -= 0.05f;
            cointimeMultiplier++;
        }*/

    }
    private void SpawnBlocks()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (randomIndex == i)
            {
                //Instantiate(coinPrefab, spawnPoints[i].position, Quaternion.identity);
                EZ_PoolManager.Spawn(coinPrefab, spawnPoints[i].position, Quaternion.identity);
            }
        }

    }
}
