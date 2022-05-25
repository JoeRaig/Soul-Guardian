using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] GameObject minionPrefab;
    [SerializeField] Transform minionPool;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemyPool;

    void Update()
    {
        InvokeMinion();
        InvokeEnemy();
    }

    void InvokeMinion()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Invoking");
            Instantiate(minionPrefab, transform.position, Quaternion.identity, minionPool);
        }
    } 
    
    void InvokeEnemy()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("Invoking");
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemyPool);
        }
    }
}
