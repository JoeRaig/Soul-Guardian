using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    Shooting shootingScript;

    float speed = 15f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shootingScript = GameObject.Find("Player").GetComponent<Shooting>();
    }

    void Start()
    {
        Vector3 direction = shootingScript.IsFacingLeft ? -transform.right : transform.right;

        rb.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
    }
}
