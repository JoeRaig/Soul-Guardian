using System.Collections;
using UnityEngine;

public class Obelisk : MonoBehaviour
{
    [SerializeField] GameObject strikePrefab;
    [SerializeField] AudioClip shootSFX;

    Animator obeliskAnim;
    Camera mainCamera;
    SFXManager sm;
    Health playerHealthScript;

    Vector2 aimPoint;
    bool canShoot = true;
    float shootDelay = 0.75f;

    void Awake()
    {
        obeliskAnim = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        sm = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();
        playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    void Update()
    {
        if (!canShoot || playerHealthScript.PlayerIsDead) return;

        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            canShoot = false;
            StartCoroutine(AttackSequence());
        }
    }

    IEnumerator AttackSequence()
    {
        sm.PlayOneShot(shootSFX, 0.5f);

        obeliskAnim.SetTrigger("Attack");
        aimPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(strikePrefab, aimPoint, Quaternion.identity);

        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
