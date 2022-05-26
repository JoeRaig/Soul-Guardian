using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    Rigidbody2D rb;
    Transform target;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        Vector2 direction = target.position - transform.position;
        transform.localScale = new Vector2(Mathf.Sign(direction.x), 1f);

        rb.velocity = direction.normalized * speed;
    }
}
