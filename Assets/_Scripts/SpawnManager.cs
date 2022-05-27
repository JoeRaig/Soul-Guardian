using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab1, enemyPrefab2, enemyPrefab3;

    Transform enemyPool;
    GameObject spawners;
    Transform[] spawnPoints = new Transform[] { };

    void Awake()
    {
        enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<Transform>();
        spawners = GameObject.FindGameObjectWithTag("Spawners");
    }

    void Start()
    {
        GetSpawnPoints();
    }

    void GetSpawnPoints()
    {
        foreach(Transform child in spawners.transform)
        {
            Debug.Log(child.transform.position);
        }
    }

    void Update()
    {
        
    }
}
