using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField] Transform body;

    Animator anim;
    Transform target;

    float moveSpeed = 1f;
    float stopRange = 1f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Obelisk").transform.GetChild(0).GetComponent<Transform>();
    }

    void Update()
    {
        FaceToTarget();
        MinionAI();
    }

    void MinionAI()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > stopRange)
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
        anim.SetBool("isRunning", true);
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    void Attack()
    {
        anim.SetBool("isAttacking", true);
    }

    void FaceToTarget()
    {
        body.localScale = new Vector2(Mathf.Sign(target.position.x - transform.position.x), 1f);
    }
}
