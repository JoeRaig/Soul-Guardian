using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int hitPoints = 10;

    public void ReduceHealth()
    {
        hitPoints--;
    }
}
