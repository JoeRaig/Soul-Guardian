using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    AudioSource audioSource;

    void Awake()
    {
        SingletonMe();

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip, float volume = 1)
    {
        if (audioSource.enabled == false) { return; }

        StopAudio();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void StopAudio()
    {
        if (audioSource.enabled == false) { return; }

        audioSource.Stop();
    }

    public void PlayOneShot(AudioClip audioClip, float volume = 1)
    {
        if (audioSource.enabled == false) { return; }

        audioSource.PlayOneShot(audioClip, volume);
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
