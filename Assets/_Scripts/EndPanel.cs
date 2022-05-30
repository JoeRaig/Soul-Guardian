using System.Collections;
using TMPro;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waves;
    [SerializeField] TextMeshProUGUI enemies;
    [SerializeField] TextMeshProUGUI minions;

    WaveCounter waveCounterScript;

    void Awake()
    {
        waveCounterScript = GameObject.FindGameObjectWithTag("WaveCounter").GetComponent<WaveCounter>();
    }

    void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    void DisplayStats()
    {
        waves.text = $"{waveCounterScript.WavesSurvived}";
        enemies.text = $"{waveCounterScript.EnemyCounter}";
        minions.text = $"{waveCounterScript.MinionCounter}";
    }

    public void InitiateEndGame()
    {
        StartCoroutine(FinishGame());
    }

    IEnumerator FinishGame()
    {
        DisplayStats();
        Cursor.visible = true;

        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
