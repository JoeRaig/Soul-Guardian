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

    bool isDialogTime = false;

    int introTextIndex = 0;
    string[] introTexts = new string[] {
        "No puede ser...han vuelto",
        "Han pasado cinco años desde la ultima vez que sentí este temblor",
        "No permitiré que se lleven las almas del bosque",
        "Debo llegar rápido al monolito...",
    }; 
    
    void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();
        mm = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        Cursor.visible = false;

        StartCoroutine(StartIntro1());
        UpdateInfoText();
    }

    void Update()
    {
        NextText();
    }

    IEnumerator StartIntro1()
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
        isDialogTime = true;
    }

    void NextText()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDialogTime)
        {
            if (introTextIndex != 3)
            {
                introTextIndex++;
                UpdateInfoText();
            }
            else
            {
                isDialogTime = false;
                infoPanel.SetActive(false);
                ChangeStatePlayerComponents();   
            }
        }
    }

    void UpdateInfoText()
    {
        infoText.text = introTexts[introTextIndex];
    }

    void ChangeStatePlayerComponents()
    {
        player.GetComponent<Movement>().enabled = true;
        var shootScript = player.GetComponent<Shooting>();
        shootScript.enabled = true;
        shootScript.CanShoot = false;
        player.transform.Find("Crosshair").gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Activate intro 2");
        }
    }
}
