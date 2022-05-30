using UnityEngine;
using UnityEngine.UI;

public class ObeliskHealth : MonoBehaviour
{
    [SerializeField] Image fillArea;
    [SerializeField] AudioClip destroySFX;
    [SerializeField] ParticleSystem destroyVFX;

    Slider obeliskHealthBar;
    Health playerHealthScript;
    SFXManager sm;

    int initialHitPoints = 1000;
    
    int currentHitPoints = 0;
    public int CurrentHitPoints { get => currentHitPoints; }

    bool isObeliskDestroy = false;

    void Awake()
    {
        obeliskHealthBar = GameObject.Find("ObeliskHealthBar").GetComponent<Slider>();
        playerHealthScript = GameObject.Find("Player").GetComponent<Health>(); 
        sm = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();
    }

    void Start()
    {
        currentHitPoints = initialHitPoints;

        DisplaySlider();
    }

    void Update()
    {
        DisplaySlider();
    }

    void DisplaySlider()
    {
        obeliskHealthBar.value = currentHitPoints * 0.001f;
    }

    public void ReduceObeliskHealth()
    {
        currentHitPoints--;

        if (currentHitPoints <= 0)
        {
            if (!isObeliskDestroy)
            {
                sm.PlayOneShot(destroySFX, 0.75f);
                destroyVFX.gameObject.SetActive(true);

                isObeliskDestroy = true;
            }

            playerHealthScript.FinishGame();
        }
    }
}
