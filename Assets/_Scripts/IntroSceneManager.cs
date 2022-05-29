using System.Collections;
using TMPro;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    [SerializeField] GameObject infoPanel;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] AudioClip introSFX;
    [SerializeField] AudioClip introSong;

    SFXManager sm;
    MusicManager mm;
    GameObject player;

    void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();
        mm = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        StartCoroutine(StartIntro());
        UpdateInfoText("");
    }

    IEnumerator StartIntro()
    {
        yield return new WaitForSeconds(1f);
        sm.PlayOneShot(introSFX, 0.75f);
        CameraShake.Instance.ShakeCamera(15f, 3f);

        yield return new WaitForSeconds(6f);
        mm.PlaySound(introSong);

        player.transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        player.transform.localScale = new Vector3(-1, 1, 1);

        yield return new WaitForSeconds(1f);
        player.transform.localScale = new Vector3(1, 1, 1);

        yield return new WaitForSeconds(0.5f);
        infoPanel.SetActive(true);
        UpdateInfoText("What was that?!?!");
        
        
        //player.GetComponent<Movement>().enabled = true;
    }

    void UpdateInfoText(string text)
    {
        infoText.text = text;
    }
}
