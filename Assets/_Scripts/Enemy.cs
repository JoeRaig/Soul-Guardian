using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] Transform[] attackPoints;
    [Range(0.1f, 1f)]
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject minionPrefab;

    Transform target;
    Animator anim;
    Health healthPlayerScript;

    int hitPoints = 3;
    float moveSpeed = 3f;
    bool isDead = false;
    float chaseStopRange = 1.25f;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        healthPlayerScript = target.GetComponent<Health>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        FaceToTarget();
        EnemyAI();
    }

    void FaceToTarget()
    {
        body.localScale = new Vector2(Mathf.Sign(target.position.x - transform.position.x), 1f);
    }

    void EnemyAI()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > chaseStopRange && !isDead)
        {
            Move();
        }
        else
        {
            if (!healthPlayerScript.PlayerIsDead)
            {
                MeleeAnimation();
            }
            else
            {
                anim.SetBool("isAttacking", false);
            }
        }
    }

    void Move()
    {
        anim.SetBool("isAttacking", false);
        anim.SetBool("isRunning", true);
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    void MeleeAnimation()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", true);
    }

    void Attack()
    {
        if (healthPlayerScript.PlayerIsDead) return;

        foreach (Transform attackPoint in attackPoints)
        {
            Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

            if (hitPlayer != null && !healthPlayerScript.PlayerIsDead)
            {
                healthPlayerScript.ReduceHealth();
            }
        }
    }

    void ReceiveDamage()
    {
        hitPoints--;

        if (hitPoints <= 0)
        {
            isDead = true;
           
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);
            anim.SetTrigger("Death");

            StartCoroutine(InvokeMinions());
        }
    }

    IEnumerator InvokeMinions()
    {
        yield return new WaitForSeconds(2f);

        Instantiate(minionPrefab, transform.position + new Vector3(1, 1, 0), Quaternion.identity);
        Instantiate(minionPrefab, transform.position + new Vector3(2, 0.5f, 0), Quaternion.identity);
        Instantiate(minionPrefab, transform.position + new Vector3(1.5f, 2, 0), Quaternion.identity);
        Destroy(gameObject);
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

    // Melee visual impact gizmo
    void OnDrawGizmosSelected()
    {
        if (attackPoints == null) return;

        foreach (Transform attackPoint in attackPoints)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
