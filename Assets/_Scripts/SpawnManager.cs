using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab1, enemyPrefab2, enemyPrefab3;

    Transform enemyPool;
    GameObject spawners;
    Transform[] spawnPoints = new Transform[10];

    bool isGameStarted = false;

    bool waveFinish = true;
    int waveNumber = 1;

    int enemyWaveCurrent = 0;
    int enemyWaveIncremental = 1;
    int enemyWaveTotal = 2;
    
    float spawnEnemyDelay = 2.5f;
    float spawnWaveDelay = 7f;

    void Awake()
    {
        enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<Transform>();
        spawners = GameObject.FindGameObjectWithTag("Spawners");
    }

    void Start()
    {
        GetSpawnPoints();

        // Refactor
        isGameStarted = true; 
    }

    void Update()
    {
        if (isGameStarted)
        {
            if (waveFinish)
            {
                waveFinish = false;
                StartCoroutine(StartNextWave());
            }  
        }
    }

    void GetSpawnPoints()
    {
        for (int i = 0; i < 10; i++)
        {
            spawnPoints[i] = spawners.transform.GetChild(i);
        }
    }

    IEnumerator StartNextWave()
    {
        waveFinish = false;

        yield return new WaitForSeconds(spawnWaveDelay);
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        Transform spawnSelected = SelectSpawnPoint();

        while (enemyWaveCurrent < enemyWaveTotal) 
        {
            Instantiate(GetRandomEnemyPrefab(), spawnSelected.position, Quaternion.identity, enemyPool);
            enemyWaveCurrent++;

            yield return new WaitForSeconds(spawnEnemyDelay);
        }

        enemyWaveCurrent = 0;
        IncrementWave();

        waveFinish = true;   
    }

    void IncrementWave()
    {
        waveNumber++;
        enemyWaveIncremental++;

        if (enemyWaveIncremental % 2 == 0) enemyWaveTotal++;

        Debug.Log(enemyWaveTotal);
    }

    GameObject GetRandomEnemyPrefab()
    {
        int randomPrefab = Random.Range(0, 3);

        switch(randomPrefab)
        {
            case 0:
                return enemyPrefab1;
            case 1:
                return enemyPrefab2;
            case 2:
                return enemyPrefab3;
            default:
                return enemyPrefab1;
        }
    }

    Transform SelectSpawnPoint()
    {
        int randomSpawn = Random.Range(0, 10);

        return spawnPoints[randomSpawn];
    }
}
