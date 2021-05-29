using UnityEngine;
using EZ_Pooling;

public class BlockSpawner : MonoBehaviour
{
    //public Transform[] spawnPoints;
    public RectTransform[] spawnPoints;
    //public GameObject blockPrefab;
    public Transform blockPrefab;
    //private float timeToSpawn = 1f;
    private float timeToSpawn = 3f;
    private float timeBetweenWaves = 1.5f;

    void Update()
    {
        if(Time.timeSinceLevelLoad >= timeToSpawn)
        {
            SpawnBlocks();
            timeToSpawn = Time.timeSinceLevelLoad + timeBetweenWaves;
        }
        
    }

    private void SpawnBlocks()
    {
        int randomSpawn = Random.Range(0, 4);

        switch (randomSpawn)
        {
            case 0:
                SpawnBlocks1();
                break;
            case 1:
                SpawnBlocks2();
                break;
            case 2:
                SpawnBlocks3();
                break;
            case 3:
                SpawnBlocks4();
                break;
        }
    }

    private void SpawnBlocks1()
    {
        int randomPos = Random.Range(0, 5);
        EZ_PoolManager.Spawn(blockPrefab, spawnPoints[randomPos].position, Quaternion.identity);

    }

    private void SpawnBlocks2()
    {
        int prevRandom = 0;
        for (int i = 0; i < 2; i++)
        {
            int randomPos = Random.Range(0, 5);
            if (randomPos != prevRandom)
            {
                EZ_PoolManager.Spawn(blockPrefab, spawnPoints[randomPos].position, Quaternion.identity);
                prevRandom = randomPos;
            }
        }
    }

    private void SpawnBlocks3()
    {
        int prevRandom = 0;
        for (int i = 0; i < 3; i++)
        {
            int randomPos = Random.Range(0, 5);
            if(randomPos != prevRandom)
            {
                EZ_PoolManager.Spawn(blockPrefab, spawnPoints[randomPos].position, Quaternion.identity);
                prevRandom = randomPos;
            }
            
        }
    }

    private void SpawnBlocks4()
    {
        int prevRandom = 0;
        for (int i = 0; i < 4; i++)
        {
            int randomPos = Random.Range(0, 5);
            if (randomPos != prevRandom)
            {
                EZ_PoolManager.Spawn(blockPrefab, spawnPoints[randomPos].position, Quaternion.identity);
                prevRandom = randomPos;
            }
        }
    }


}
