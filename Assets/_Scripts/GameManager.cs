using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip buttonSFX;
    [SerializeField] AudioClip mainMenuSong;
    [SerializeField] AudioClip introSong;
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
        
    }

    public void Loadgame()
    {
        sm.PlayOneShot(buttonSFX);
        mm.PlaySound(introSong);
        SceneManager.LoadScene("Game");
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
