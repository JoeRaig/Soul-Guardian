using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip buttonSFX;

    public static GameManager instance;

    SFXManager sm;

    bool isGameStarted = false;

    void Awake()
    {
        SingletonMe();

        sm = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Loadgame()
    {
        sm.PlayOneShot(buttonSFX);
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
