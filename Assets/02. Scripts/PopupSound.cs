using System;
using UnityEngine;

public class PopupSound : MonoBehaviour
{ 
    public AudioSource closeSource;
    public AudioSource xSource;
    
    private void Awake()
    {
        Mute();
    }

    public void Mute()
    {
        bool mute = SoundManager.Instance.effectMute;
        closeSource.mute = mute;
        xSource.mute = mute;
    }
}
