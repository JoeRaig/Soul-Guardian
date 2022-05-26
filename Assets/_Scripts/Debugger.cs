using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] GameObject minionPrefab;
    [SerializeField] Transform minionPool;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemyPool;
    [SerializeField] GameObject bulletPrefab;

    void Update()
    {
        InvokeMinion();
        InvokeEnemy();
        InvokeBullet();
    }

    void InvokeMinion()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Instantiate(minionPrefab, transform.position, Quaternion.identity, minionPool);
        }
    } 
    
    void InvokeEnemy()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemyPool);
        }
    }

    void InvokeBullet()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }
}
