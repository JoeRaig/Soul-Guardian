using System.Collections;
using TMPro;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    [SerializeField] GameObject infoPanel;
    [SerializeField] GameObject indicatorPanel;
    [SerializeField] GameObject startPanel;
    [SerializeField] TextMeshProUGUI indicatorText;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] AudioClip introSFX;
    [SerializeField] AudioClip introSong;
    [SerializeField] GameObject playerWeapon;
    [SerializeField] Obelisk obeliskScript;

    SFXManager sm;
    MusicManager mm;
    GameObject player;
    Movement movement;
    Shooting shooting;

    bool isDialogTime = false;
    bool isIntro2Started = false;

    int introTextIndex = 0;
    string[] introTexts1 = new string[] {
        "That rumbling… It has been five years since the last time it happened.",
        "They have found it again…",
        "I will not let them steal the souls, even if it means fighting my last battle.",
        "I must get to the obelisk quickly!",
        "A pathfinder! Damn, they beat me to it!",
        "It did not see me, it must be in some kind of trance while sending the location.",
        "Maybe it is not too late. I will try to finish him off with my staff.",
        "It is infected with evil wraiths. My magic will not affect them.",
        "They are being lured by the souls in the obelisk. I will channel its power so that I can finish them off."
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
            else if (introTextIndex == 6)
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
            else if (introTextIndex == 8)
            {
                infoPanel.SetActive(false);
                obeliskScript.enabled = true;
                StartCoroutine(ThirdIndicatorPanel());
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

        yield return new WaitForSeconds(0f);
    }

    IEnumerator FirstIndicatorPanel()
    {
        yield return new WaitForSeconds(1f);
        indicatorPanel.SetActive(true);

        yield return new WaitForSeconds(10f);
        indicatorPanel.SetActive(false);
    }

    IEnumerator SecondIndicatorPanel()
    {
        indicatorText.text = "CAST PROJECTILE - LEFT MOUSE CLICK";

        yield return new WaitForSeconds(1.5f);
        indicatorPanel.SetActive(true);
    }

    IEnumerator ThirdIndicatorPanel()
    {
        indicatorText.text = "CAST PROJECTILE - LEFT MOUSE CLICK" +
            "\nLIGHTNING STRIKE - RIGHT MOUSE CLICK";

        yield return new WaitForSeconds(1f);
        indicatorPanel.SetActive(true);

        yield return new WaitForSeconds(2.5f);
        startPanel.SetActive(true);
    }


    public void ActivateIntro3()
    {
        StartCoroutine(StartIntro3());
    }

    IEnumerator StartIntro3()
    {
        yield return new WaitForSeconds(2f);

        indicatorPanel.SetActive(false);
        infoPanel.SetActive(true);
        isDialogTime = true;
    }
}
