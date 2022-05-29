using UnityEngine;
using UnityEngine.UI;

public class ObeliskHealth : MonoBehaviour
{
    [SerializeField] Image fillArea;
    Slider obeliskHealthBar;

    int initialHitPoints = 1000;
    
    int currentHitPoints = 0;
    public int CurrentHitPoints { get => currentHitPoints; set => currentHitPoints = value; }

    
    void Awake()
    {
        obeliskHealthBar = GameObject.Find("ObeliskHealthBar").GetComponent<Slider>();
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
}
