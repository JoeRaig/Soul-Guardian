using System.Collections;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [SerializeField] Transform body;

    Animator anim;
    Transform target;

    bool isActive = false;
    float moveSpeed = 1f;
    float stopRange = 1f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Obelisk").transform.GetChild(0).GetComponent<Transform>();
    }

    void Start()
    {
        StartCoroutine(InitializeMinion());   
    }

    void Update()
    {
        FaceToTarget();

        if (isActive) MinionAI();
    }

    IEnumerator InitializeMinion()
    {
        yield return new WaitForSeconds(0.5f);
        body.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.75f);
        isActive = true;
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
