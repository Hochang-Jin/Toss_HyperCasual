using System;
using UnityEngine;

public class EffectSource : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSource.mute = SoundManager.Instance.effectSource.mute;
    }
}
