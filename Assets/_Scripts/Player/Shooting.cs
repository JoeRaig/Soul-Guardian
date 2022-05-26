using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject bulletPrefab;

    Camera mainCamera;
    Health healthScript;

    Vector3 aimDirection;
    float rotationZ;

    bool isFacingLeft = false;
    public bool IsFacingLeft { get => isFacingLeft; }

    bool canShoot = true;
    float shootDelay = 0.4f;

    void Awake()
    {
        Cursor.visible = false;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        healthScript = GetComponent<Health>();
    }

    void Update()
    {
        if (healthScript.PlayerIsDead) return;

        RotateWeapon();
        CrosshairAim();
        FaceMouseDirection();

        if (canShoot)
        {
            Shoot();
        }
    }

    void FaceMouseDirection()
    {
        isFacingLeft = Mathf.Sign(aimDirection.x) < 0;
        weapon.localScale = new Vector2(Mathf.Sign(aimDirection.x), 1f);
    }

    void RotateWeapon()
    {
        aimDirection = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        aimDirection.Normalize();

        if (isFacingLeft)
        {
            rotationZ = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 180f;
        }
        else
        {
            rotationZ = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        }

        weapon.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    void CrosshairAim()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        crosshair.transform.position = mousePos;
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            canShoot = false;
            StartCoroutine(ShootSequence());
        }
    }

    IEnumerator ShootSequence()
    {
        bulletPrefab.transform.localScale = new Vector3(isFacingLeft ? -1 : 1, transform.localScale.y, transform.localScale.z);
        Instantiate(bulletPrefab, weapon.transform.position, weapon.rotation);

        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
