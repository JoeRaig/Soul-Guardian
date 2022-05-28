using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] SpriteRenderer healthBarSprite;
    [SerializeField] int hitPoints = 3;
    public int HitPoints { get => hitPoints; }

    Sprite[] sprites;

    int spriteTotalHitPoints;
    int displayIndex = 0;

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
        hitPoints--;
        displayIndex++;
    }
}
