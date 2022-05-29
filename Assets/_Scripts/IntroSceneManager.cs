using System.Collections;
using TMPro;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    [SerializeField] GameObject infoPanel;
    [SerializeField] GameObject indicatorPanel;
    [SerializeField] TextMeshProUGUI indicatorText;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] AudioClip introSFX;
    [SerializeField] AudioClip introSong;
    [SerializeField] GameObject playerWeapon;

    SFXManager sm;
    MusicManager mm;
    GameObject player;
    Movement movement;
    Shooting shooting;

    bool isDialogTime = false;
    bool isIntro2Started = false;

    int introTextIndex = 0;
    string[] introTexts1 = new string[] {
        "Ese estruendo...Han pasado cinco años desde la ultima batalla",
        "Lo han vuelto a encontrar...",
        "No permitiré que se lleven las almas",
        "Debo llegar rápido al monolito...",
        "¡Un explorador! Maldita sea, se me han adelantado",
        "No me ha visto, debe estar en trance mientras transmite la localizacion",
        "Acabaré con él a distancia con mi bastón, quizá aún no sea demasiado tarde",
        "",
    };
    
    void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();
        mm = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<Movement>();
        shooting = player.GetComponent<Shooting>();
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
            if (introTextIndex == 3)
            {
                IncrementText();
                isDialogTime = false;
                infoPanel.SetActive(false);
                ChangeStatePlayerComponents();

                StartCoroutine(FirstIndicatorPanel());
            }
            else if(introTextIndex == 6)
            {
                IncrementText();
                isDialogTime = false;
                infoPanel.SetActive(false);

                movement.enabled = true;
                shooting.enabled = true;
                shooting.CanShoot = true;
                playerWeapon.SetActive(true);

                StartCoroutine(SecondIndicatorPanel());
            }
            else
            {
                IncrementText();
            }
        }
    }

    void IncrementText()
    {
        introTextIndex++;
        UpdateInfoText();
    }

    void UpdateInfoText()
    {
        infoText.text = introTexts1[introTextIndex];
    }

    void ChangeStatePlayerComponents()
    {
        movement.enabled = true;
        shooting.enabled = true;
        shooting.CanShoot = false;
        player.transform.Find("Crosshair").gameObject.SetActive(true);
    }

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Activate it!");
            if (!isIntro2Started)
            {
                StartCoroutine(StartIntro2());
            }
        }
    }

    IEnumerator StartIntro2()
    {
        isIntro2Started = true;
        indicatorPanel.SetActive(false);
        infoPanel.SetActive(true);
        isDialogTime = true;
        movement.StopPlayer();
        movement.enabled = false;

        yield return new WaitForSeconds(1f);
    }

    IEnumerator FirstIndicatorPanel()
    {
        yield return new WaitForSeconds(1.5f);
        indicatorPanel.SetActive(true);

        yield return new WaitForSeconds(10f);
        indicatorPanel.SetActive(false);
    }

    IEnumerator SecondIndicatorPanel()
    {
        indicatorText.text = "CAST - LEFT MOUSE CLICK";

        yield return new WaitForSeconds(1.5f);
        indicatorPanel.SetActive(true);
    }
}
