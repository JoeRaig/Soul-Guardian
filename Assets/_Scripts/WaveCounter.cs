using TMPro;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{
    TextMeshProUGUI counterText;
    SpawnManager spawnManagerScript;

    int enemyCounter;
    public int EnemyCounter { get => enemyCounter; }

    int minionCounter;
    public int MinionCounter { get => minionCounter; }

    int waveCounter;
    public int WavesSurvived { get => waveCounter; }


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
    }

    void DisplayWaveCounter()
    {
        waveCounter = spawnManagerScript.WaveNumber;
        counterText.text = "Wave: " + waveCounter;
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
