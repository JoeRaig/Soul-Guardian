using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] Transform weapon;
    [SerializeField] GameObject crosshair;

    Camera mainCamera;
    Rigidbody2D rb;
    Animator anim;

    float moveSpeed = 5f;
    float xAxis;
    float yAxis;
    Vector2 moveDirection;
    Vector3 mousePosition;
    Vector3 aimDirection;
    float aimLength = 3f;
    bool isFacingLeft = false;


    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
        CheckIfMoving();
        FaceMouseDirection();
        RotateWeapon();
        CrosshairAim();
    }

    void Move()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(xAxis, yAxis).normalized;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = mousePosition - transform.position;

        rb.velocity = moveDirection * moveSpeed;
    }

    void CheckIfMoving()
    {
        if (xAxis != 0 || yAxis != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    void FaceMouseDirection()
    {
        isFacingLeft = Mathf.Sign(aimDirection.x) < 0;

        body.localScale = new Vector2(Mathf.Sign(aimDirection.x), 1f);
        weapon.localScale = new Vector2(Mathf.Sign(aimDirection.x), 1f);
    }

    void RotateWeapon()
    {
        float rotationZ;

        Vector3 difference = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        difference.Normalize();

        if (isFacingLeft)
        {
            rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 125f;
        }
        else
        {
            rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 55f;
        }

        weapon.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    void CrosshairAim()
    {
        Vector3 aim = new Vector3(aimDirection.x, mousePosition.y - transform.position.y, 0);

        aim.Normalize();
        aim *= aimLength;
        crosshair.transform.localPosition = aim;
    }
}
