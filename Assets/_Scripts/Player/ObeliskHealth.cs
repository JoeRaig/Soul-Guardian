using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskHealth : MonoBehaviour
{
    int initialHitPoints = 1000;
    
    int currentHitPoints = 0;
    public int CurrentHitPoints { get => currentHitPoints; set => currentHitPoints = value; }

    void Start()
    {
        currentHitPoints = initialHitPoints;
    }

    void Update()
    {
        Debug.Log(currentHitPoints);

        if (currentHitPoints <= 0)
        {
            Debug.Log("OBELISK DESTROY!!");
        }
    }
}
