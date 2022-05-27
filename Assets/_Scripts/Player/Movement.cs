using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Transform body;

    Camera mainCamera;
    Rigidbody2D rb;
    Animator anim;

    float moveSpeed = 6f;
    float xAxis;
    float yAxis;
    Vector2 moveDirection;
    Vector3 mousePosition;
    Vector3 aimDirection;
    
    void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        ProcessInputs();
        CheckIfMoving();
        FaceMouseDirection();
    }

    void ProcessInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(xAxis, yAxis).normalized;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = mousePosition - transform.position;

        Move();
    }

    void Move()
    {
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
        body.localScale = new Vector2(Mathf.Sign(aimDirection.x), 1f);
    }
}
