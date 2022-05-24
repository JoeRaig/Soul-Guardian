using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject crosshair;

    Animator anim;

    int hitPoints = 5;
    bool playerIsDead = false;
    public bool PlayerIsDead { get => playerIsDead; }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ReduceHealth()
    {
        anim.SetTrigger("Hit");
        hitPoints--;

        if (hitPoints <= 0)
        {
            Death();
            DisablePlayerFunctionality();
        }
    }

    void Death()
    {
        playerIsDead = true;
        anim.SetTrigger("Death");
    }

    void DisablePlayerFunctionality()
    {
        Destroy(gameObject.GetComponent<Movement>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        anim.SetBool("isRunning", false);
        weapon.SetActive(false);
        crosshair.SetActive(false);
    }
}
