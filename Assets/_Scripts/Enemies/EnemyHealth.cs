using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] SpriteRenderer healthBarSprite;
    [SerializeField] int hitPoints = 3;
    [SerializeField] AudioClip enemyHitSFX;
    public int HitPoints { get => hitPoints; }

    Sprite[] sprites;
    SFXManager sm;

    int spriteTotalHitPoints;
    int displayIndex = 0;

    void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();
    }

    void Start()
    {
        spriteTotalHitPoints = hitPoints;
    }

    void Update()
    {
        DisplayHealthBarSprite();
    }

    void DisplayHealthBarSprite()
    {
        sprites = Resources.LoadAll<Sprite>($"Sprites/Health Bars/health_bar_{spriteTotalHitPoints}");
        healthBarSprite.sprite = sprites[displayIndex];
    }

    public void ReduceEnemyHealth()
    {
        sm.PlayOneShot(enemyHitSFX, 0.5f);

        hitPoints--;
        displayIndex++;
    }
}
