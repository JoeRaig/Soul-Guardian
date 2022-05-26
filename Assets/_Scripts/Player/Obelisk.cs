using System.Collections;
using UnityEngine;

public class Obelisk : MonoBehaviour
{
    [SerializeField] GameObject strikePrefab;

    Animator obeliskAnim;
    Camera mainCamera;

    Vector2 aimPoint;
    bool canShoot = true;
    float shootDelay = 0.75f;

    void Awake()
    {
        obeliskAnim = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (!canShoot) return;

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
        obeliskAnim.SetTrigger("Attack");
        aimPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(strikePrefab, aimPoint, Quaternion.identity);

        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
