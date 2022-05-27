using System.Collections;
using UnityEngine;

public class EnemyDistance : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] Transform shootPoint;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject minionPrefab;
    [SerializeField] ParticleSystem deathVFX;
    [SerializeField] ParticleSystem impactVFX;
    [SerializeField] int minionAmount;
    [SerializeField] int hitPoints = 3;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float chaseStopRange = 1.25f;
    [SerializeField] float shotDelay = 3f;
    [SerializeField] float hideBodyDelay = 0.3f;

    Transform target;
    Animator anim;
    Health healthPlayerScript;
    Transform minionPool;
    Transform bulletPool;

    bool isActive = false;
    bool isDead = false;
    bool canShoot = false;
    float timeNextShot;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        healthPlayerScript = target.GetComponent<Health>();
        minionPool = GameObject.FindGameObjectWithTag("MinionPool").GetComponent<Transform>();
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<Transform>();
    }

    void Start()
    {
        timeNextShot = shotDelay;
        StartCoroutine(SummonEnemy());
    }

    void Update()
    {
        if (isActive && !isDead)
        {
            CalculateShootState();
            FaceToTarget();
            EnemyAI();
        }
    }

    IEnumerator SummonEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        body.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.75f);
        isActive = true;
    }

    void FaceToTarget()
    {
        body.localScale = new Vector2(Mathf.Sign(target.position.x - transform.position.x), 1f);
    }

    void CalculateShootState()
    {
        timeNextShot -= Time.deltaTime;

        if (timeNextShot <= 0)
        {
            canShoot = true;
            timeNextShot = shotDelay;
        }
    }

    void EnemyAI()
    {
        if (canShoot && !healthPlayerScript.PlayerIsDead)
        {
            ShootAnimation();    
        }
        else if (healthPlayerScript.PlayerIsDead)
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isRunning", false);
        }
        else
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget > chaseStopRange && !isDead)
            {
                Move();
            }
            else
            {
                anim.SetBool("isAttacking", false);
                anim.SetBool("isRunning", false);
            }
        }
    }

    void Move()
    {
        anim.SetBool("isAttacking", false);
        anim.SetBool("isRunning", true);
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    void ShootAnimation()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", true);
    }

    void Shoot()
    {
        if (healthPlayerScript.PlayerIsDead) return;

        Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, bulletPool);
        canShoot = false;
    }

    void ReceiveDamage()
    {
        InstantiateImpactVFX();
        hitPoints--;

        if (hitPoints <= 0)
        {
            isDead = true;

            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);
            anim.SetTrigger("Death");

            StartCoroutine(DeathSequence());
        }
    }

    void InstantiateImpactVFX()
    {
        Instantiate(impactVFX, transform.position, Quaternion.FromToRotation(transform.position, target.position), transform);
    }

    IEnumerator DeathSequence()
    {
        if (deathVFX != null)
        {
            deathVFX.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            deathVFX.Play();
        }

        yield return new WaitForSeconds(hideBodyDelay);
        body.gameObject.SetActive(false);

        InvokeMinions();

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void InvokeMinions()
    {
        for (int i = 0; i < minionAmount; i++)
        {
            Vector3 position = transform.position + new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f));

            Instantiate(minionPrefab, position, Quaternion.identity, minionPool);
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
