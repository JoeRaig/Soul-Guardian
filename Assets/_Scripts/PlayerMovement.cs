using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Camera sceneCamera;
    [SerializeField] Transform weapon;

    Rigidbody2D rb;
    Animator anim;

    float xAxis;
    float yAxis;
    Vector2 moveDirection;
    Vector2 mousePosition;
    float aimDirection;
    bool isFacingLeft = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
        CheckIfMoving();
        FaceMouseDirection();
        WeaponRotation();
    }

    void Move()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(xAxis, yAxis).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = mousePosition.x - rb.position.x;

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
        transform.localScale = new Vector2(Mathf.Sign(aimDirection), 1f);
        isFacingLeft = Mathf.Sign(aimDirection) < 0;
    }

    void WeaponRotation()
    {
        float aimAngle;

        if (isFacingLeft)
        {
            aimAngle = Mathf.Atan2(aimDirection, mousePosition.y) * Mathf.Rad2Deg + 10f;
        }
        else
        {
            aimAngle = Mathf.Atan2(mousePosition.y, aimDirection) * Mathf.Rad2Deg - 55f;
        }

        weapon.localRotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
    }
}
