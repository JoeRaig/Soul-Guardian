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
        Debug.Log("Hit - " + hitPoints);

        anim.SetTrigger("Hit");
        hitPoints--;

        if (hitPoints <= 0)
        {
            Death();
            DisablePlayerFunctionalities();
        }
    }

    void Death()
    {
        playerIsDead = true;
        anim.SetTrigger("Death");
    }

    void DisablePlayerFunctionalities()
    {
        weapon.SetActive(false);
        crosshair.SetActive(false);
    }
}
