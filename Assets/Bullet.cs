using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    float speed = 25f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Start()
    {
        rb.velocity = transform.right * speed;
    }
}
