using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] Transform attackPoints;
    [Range(0.1f, 1f)]
    [SerializeField] float attackRange = 0.5f;

    Transform target;
    Animator anim;

    int hitPoints = 3;
    float moveSpeed = 3f;
    bool isDead = false;
    float chaseStopRange = 1.25f;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        FaceToTarget();
        DecideBehaviour();
    }

    void FaceToTarget()
    {
        body.localScale = new Vector2(Mathf.Sign(target.position.x - transform.position.x), 1f);
    }

    void DecideBehaviour()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > chaseStopRange)
        {
            Move();
        }
        else
        {
            Attack();
        }
    }

    void Move()
    {
        anim.SetBool("isAttacking", false);
        anim.SetBool("isRunning", true);
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    void Attack()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", true);

        // Refactor
        //Physics2D.OverlapCircleAll(attackPoints.position, attackRange, );

    }

    void ReceiveDamage()
    {
        hitPoints--;
        if (hitPoints <= 0)
        {
            isDead = true;
            anim.SetTrigger("Death");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" && !isDead)
        {
            anim.SetTrigger("Hit");
            ReceiveDamage();
            Destroy(collision.gameObject);
        } 
    }
}
