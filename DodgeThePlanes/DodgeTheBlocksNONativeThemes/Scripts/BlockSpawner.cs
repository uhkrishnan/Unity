using UnityEngine;
using EZ_Pooling;

public class BlockSpawner : MonoBehaviour
{
    //public Transform[] spawnPoints;
    public RectTransform[] spawnPoints;
    public Transform blockPrefab;
    private float timeToSpawn;
    private float timeBetweenWaves;
    private int previous;
    private float screenPart;

    private void Start()
    {
        previous = 0;
        timeBetweenWaves = 1.5f;
        timeToSpawn = 5f;
        /*
        screenPart = Screen.width / 6f;
        
        spawnPoints[0].localPosition = new Vector3(-screenPart * 2, 0, 0);
        spawnPoints[1].localPosition = new Vector3(-screenPart, 0, 0);
        spawnPoints[2].localPosition = new Vector3(0, 0, 0);
        spawnPoints[3].localPosition = new Vector3(screenPart, 0, 0);
        spawnPoints[4].localPosition = new Vector3(screenPart * 2, 0, 0);
        */
    }

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
        
        if(previous == 3)
        {
            randomSpawn = Random.Range(0, 2);
        }

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
        
        previous = randomSpawn;
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
