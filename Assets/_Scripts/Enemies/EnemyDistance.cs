using System.Collections;
using UnityEngine;

public class EnemyDistance : MonoBehaviour
{
    [SerializeField] GameObject body;
    [SerializeField] GameObject healthBar;
    [SerializeField] Transform shootPoint;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] GameObject minionPrefab;
    [SerializeField] ParticleSystem deathVFX;
    [SerializeField] ParticleSystem impactVFX;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] int minionAmount;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float chaseStopRange = 1.25f;
    [SerializeField] float shootRange = 10f;
    [SerializeField] float shootDelay = 3f;
    [SerializeField] float hideBodyDelay = 0.3f;
    
    Transform target;
    Animator anim;
    Health healthPlayerScript;
    EnemyHealth enemyHealthScript;
    Transform minionPool;
    Transform bulletPool;
    SFXManager sm;
    WaveCounter waveCounter;

    bool isActive = false;
    bool isDead = false;
    bool canShoot = false;
    float timeNextShot;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        healthPlayerScript = target.GetComponent<Health>();
        enemyHealthScript = GetComponent<EnemyHealth>();
        minionPool = GameObject.FindGameObjectWithTag("MinionPool").GetComponent<Transform>();
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<Transform>();
        sm = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();
        waveCounter = GameObject.FindGameObjectWithTag("WaveCounter").GetComponent<WaveCounter>();
    }

    void Start()
    {
        timeNextShot = shootDelay;
        StartCoroutine(SummonEnemy());
        Physics2D.IgnoreCollision(target.GetComponent<Collider2D>(), GetComponent<Collider2D>());
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
        body.SetActive(true);
        healthBar.SetActive(true);

        yield return new WaitForSeconds(0.75f);
        isActive = true;
    }

    void FaceToTarget()
    {
        body.transform.localScale = new Vector2(Mathf.Sign(target.position.x - transform.position.x), 1f);
    }

    void CalculateShootState()
    {
        timeNextShot -= Time.deltaTime;

        if (timeNextShot <= 0)
        {
            canShoot = true;
            timeNextShot = shootDelay;
        }
    }

    void EnemyAI()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > shootRange)
        {
            Move();
        }
        else if (canShoot && !healthPlayerScript.PlayerIsDead)
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
        enemyHealthScript.ReduceEnemyHealth();

        if (enemyHealthScript.HitPoints <= 0)
        {
            isDead = true;

            healthBar.SetActive(false);

            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", false);
            anim.SetTrigger("Death");

            waveCounter.IncreaseEnemyCounter();

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
        body.SetActive(false);

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

    void PlayShootSFX()
    {
        sm.PlayOneShot(shootSFX, 0.8f);
    }
}
