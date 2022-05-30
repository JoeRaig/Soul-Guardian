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

        SceneManager.LoadScene("Intro");
    }

    public void LoadGame()
    {
        Debug.Log("Restarting game");

        Time.timeScale = 1;
        mm.PlaySound(combatSong);
        SceneManager.LoadScene("Game");
    }

    public void SkipIntro()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1) return;

        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void QuitGame()
    {
        sm.PlayOneShot(buttonSFX);
        Application.Quit();
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
