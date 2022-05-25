using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] GameObject minionPrefab;
    [SerializeField] Transform minionPool;

    void Update()
    {
        InvokeMinion();    
    }

    void InvokeMinion()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Invoking");
            Instantiate(minionPrefab, transform.position, Quaternion.identity, minionPool);
        }
    }
}
