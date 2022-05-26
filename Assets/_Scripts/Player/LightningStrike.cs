using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    public LayerMask minionLayer;
    CapsuleCollider2D capsuleCollider;

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();   
    }

    void Start()
    {
        Invoke("AutoDestroy", 3f);  
    }

    void DealDamage()
    {
        // Set the contact filter to all object beloging to minionLayer
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        filter.SetLayerMask(minionLayer);
        
        // Get a list of these minions between the range
        List<Collider2D> minionsInRange = new List<Collider2D>();

        Physics2D.OverlapCollider(capsuleCollider, filter, minionsInRange);

        foreach (var item in minionsInRange)
        {
            item.GetComponent<Minion>().ReduceHitPoints();
        }
    }

    void AutoDestroy()
    {
        Destroy(gameObject);
    }
}
