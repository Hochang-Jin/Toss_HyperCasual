using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance{ get; private set; }
    
    public AudioSource backgroundSource;
    public AudioSource timerSource;
    
    public AudioClip backgroundMusic;
    public AudioClip timerSound;
    public AudioClip reverseTimerSound;
    public AudioClip dropSound;
    public AudioClip mergeSound;
    
    public Button muteButton;
    private Image muteImage;

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
        muteImage = muteButton.GetComponent<Image>();
        muteButton.onClick.AddListener(MuteBGM);
    }

    private void MuteBGM()
    {
        float alpha = backgroundSource.mute ? 1f : 0.3f;
        muteImage.color = new Color(1, 1, 1, alpha);
        backgroundSource.mute = !backgroundSource.mute;
        timerSource.mute = !timerSource.mute;
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
        backgroundSource.PlayOneShot(dropSound);
    }

    public void MergeSound()
    {
        backgroundSource.PlayOneShot(mergeSound);
    }
}
