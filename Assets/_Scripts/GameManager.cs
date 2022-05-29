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
        SceneManager.LoadScene("Game");
    }

    public void SkipIntro()
    {
        isGameStarted = true;
        LoadGame();
    }

    public void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameStarted)
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void ShowSettings()
    {
        sm.PlayOneShot(buttonSFX);
        Debug.Log("Settings");
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
