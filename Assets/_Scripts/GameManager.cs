using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip buttonSFX;
    [SerializeField] AudioClip mainMenuSong;
    [SerializeField] AudioClip combatSong;

    public static GameManager instance;

    SFXManager sm;
    MusicManager mm;

    bool isGameStarted = false;

    void Awake()
    {
        SingletonMe();

        sm = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();
        mm = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
    }

    void Start()
    {
        mm.PlaySound(mainMenuSong);
    }

    void Update()
    {
        SkipIntro();
        RestartGame();
    }

    public void LoadIntroScene()
    {
        mm.StopAudio();
        sm.PlayOneShot(buttonSFX);

        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        Time.timeScale = 1;
        mm.PlaySound(combatSong);
        SceneManager.LoadScene(2);
    }

    public void SkipIntro()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            isGameStarted = true;
            LoadGame();
        }
    }

    public void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameStarted)
        {
            LoadGame();
        }
    }

    void SingletonMe()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
