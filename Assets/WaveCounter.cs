using TMPro;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{
    TextMeshProUGUI counterText;
    SpawnManager spawnManagerScript;

    int enemyCounter;
    int minionCounter;

     void Awake()
    {
        counterText = GetComponent<TextMeshProUGUI>();
        spawnManagerScript = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
    }

    void Start()
    {
        DisplayWaveCounter();

        enemyCounter = 0;
        minionCounter = 0;
    }

    void Update()
    {
        DisplayWaveCounter();

        Debug.Log("Enemies: " + enemyCounter);
        Debug.Log("Minions: " + minionCounter);
    }

    void DisplayWaveCounter()
    {
        counterText.text = "Wave: " + spawnManagerScript.WaveNumber;
    }

    public void IncreaseEnemyCounter()
    {
        enemyCounter++;
    }

    public void IncreaseMinionCounter()
    {
        minionCounter++;
    }
}
