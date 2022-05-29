using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject hitPointImage;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] AudioClip gameOverMusic;

    Animator anim;
    Camera mainCamera;
    GameObject playerHealthUI;
    MusicManager mm;
    SFXManager sm;

    int hitPoints = 5;
    bool playerIsDead = false;
    public bool PlayerIsDead { get => playerIsDead; }

    void Awake()
    {
        anim = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerHealthUI = GameObject.FindGameObjectWithTag("PlayerHealthUI");
        sm = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();
        mm = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
    }

    void Start()
    {
        GenerateHeartIcons();
    }

    void GenerateHeartIcons()
    {
        for (int i = 0; i < hitPoints; i++)
        {
            Vector2 testPos = mainCamera.ViewportToScreenPoint(new Vector2(0.125f, 0.06f));
            Vector2 xPadding = mainCamera.ViewportToScreenPoint(new Vector2(0.022f, 0f));

            Instantiate(hitPointImage, testPos + (xPadding * i), Quaternion.identity, playerHealthUI.transform);
        }
    }

    public void ReduceHealth()
    {

        sm.PlayOneShot(hitSFX, 0.75f);

        anim.SetTrigger("Hit");

        Destroy(playerHealthUI.transform.GetChild(hitPoints - 1).gameObject);

        CameraShake.Instance.ShakeCamera(10f, 0.1f);

        hitPoints--;

        if (hitPoints <= 0)
        {
            mm.PlaySound(gameOverMusic);

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Shot" && !playerIsDead)
        {
            Destroy(collision.gameObject);
            ReduceHealth();
        }
    }
}
