using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class BlockSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    //public GameObject blockPrefab;
    public Transform blockPrefab;
    private float timeToSpawn = 1f;
    private float timeBetweenWaves = 2f;
    private int timeMultiplier = 5;

    void Update()
    {
        if(Time.time >= timeToSpawn)
        {
            SpawnBlocks();
            timeToSpawn = Time.time + timeBetweenWaves;
        }
        
        if (Time.time > 120)
        {
            timeBetweenWaves = 1f;
        }
        
    }
    private void SpawnBlocks()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if ((randomIndex != i))// && (randomIndex != i+2))
            {
                //Instantiate(blockPrefab, spawnPoints[i].position, Quaternion.identity);
                EZ_PoolManager.Spawn(blockPrefab, spawnPoints[i].position, Quaternion.identity);
            }
        }

    }
}
