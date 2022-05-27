using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab1;
    [SerializeField] GameObject enemyPrefab2;
    [SerializeField] GameObject enemyPrefab3;
    [SerializeField] Transform enemyPool;

    void Update()
    {
        InvokeThing1();
        InvokeThing2();
        InvokeThing3();
    }

    void InvokeThing1()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(enemyPrefab1, transform.position, Quaternion.identity, enemyPool);
        }
    } 
    
    void InvokeThing2()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(enemyPrefab2, transform.position, Quaternion.identity, enemyPool);
        }
    }

    void InvokeThing3()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(enemyPrefab3, transform.position, Quaternion.identity, enemyPool);
        }
    }
}
