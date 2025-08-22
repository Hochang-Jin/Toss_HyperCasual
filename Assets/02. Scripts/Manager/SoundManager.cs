using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance{ get; private set; }
    
    public AudioSource backgroundSource;
    public AudioSource timerSource;
    public AudioSource effectSource;
    public AudioSource settingSource;
    public AudioSource settingSourceHorizontal;
    
    public AudioClip backgroundMusic;
    public AudioClip endingMusic;
    public AudioClip timerSound;
    public AudioClip reverseTimerSound;
    public AudioClip dropSound;
    public AudioClip mergeSound;
    public AudioClip rotationSound;

    public bool bgmMute = false;
    public bool effectMute = false;

    public Transform popup;

    private void Awake()
    {
        // singleton 
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetBGM();
    }

    public void MuteBGM()
    {
        backgroundSource.mute = !backgroundSource.mute;
        bgmMute = !bgmMute;
    }

    public void MuteEffect()
    {
        timerSource.mute = !timerSource.mute;
        effectSource.mute = !effectSource.mute;
        settingSource.mute = !settingSource.mute;
        settingSourceHorizontal.mute = !settingSourceHorizontal.mute;
        effectMute = !effectMute;
    }

    public void ResetBGM()
    {
        backgroundSource.clip = backgroundMusic;
        backgroundSource.Play();
    }

    public void BGMOff()
    {
        backgroundSource.Stop();
        timerSource.Stop();
    }
    
    public void TimerOn()
    {
        timerSource.clip = timerSound;
        timerSource.Play();
    }

    public void ReverseTimer()
    {
        timerSource.clip = reverseTimerSound;
        timerSource.Play();
    }

    public void TimerOff()
    {
        timerSource.Stop();
    }
    
    public void DropSound()
    {
        effectSource.PlayOneShot(dropSound);
    }

    public void RotationSound()
    {
        effectSource.PlayOneShot(rotationSound);
    }
    
    public void MergeSound()
    {
        effectSource.PlayOneShot(mergeSound);
    }

    public void EndingSound()
    {
        backgroundSource.clip = endingMusic;
        backgroundSource.Play();
    }
}
