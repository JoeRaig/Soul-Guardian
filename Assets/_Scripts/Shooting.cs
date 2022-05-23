using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] GameObject crosshair;

    Camera mainCamera;

    Vector3 aimDirection;
    float aimLength = 3f;
    bool isFacingLeft = false;

    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        RotateWeapon();
        CrosshairAim();
        FaceMouseDirection();
    }

    void FaceMouseDirection()
    {
        isFacingLeft = Mathf.Sign(aimDirection.x) < 0;
        weapon.localScale = new Vector2(Mathf.Sign(aimDirection.x), 1f);
    }

    void RotateWeapon()
    {
        float rotationZ;

        aimDirection = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        aimDirection.Normalize();

        if (isFacingLeft)
        {
            rotationZ = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 125f;
        }
        else
        {
            rotationZ = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 55f;
        }

        weapon.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    void CrosshairAim()
    {
        Vector3 aim = new Vector3(aimDirection.x, aimDirection.y - transform.position.y, 0);

        aim.Normalize();
        aim *= aimLength;
        crosshair.transform.localPosition = aim;
    }
}
