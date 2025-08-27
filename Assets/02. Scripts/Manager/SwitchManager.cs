using System;
using Ricimi;
using UnityEngine;
using UnityEngine.UI;

public class SwitchManager : MonoBehaviour
{
    public bool bgm;
    private Slider m_musicSlider;
    private bool isStart = true;

    private void Start()
    {
        m_musicSlider = GetComponent<Slider>();
        if (bgm)
        {
            int value = SoundManager.Instance.bgmMute ? 0 : 1;
            m_musicSlider.value = value;
        }
        else
        {
            int value = SoundManager.Instance.effectMute ? 0 : 1;
            m_musicSlider.value = value;
        }
        isStart = false;
    }

    public void BGMSetting()
    {
        if (isStart) return;
        if (bgm)
        {
            SoundManager.Instance.MuteBGM();
        }
        else
        {
            SoundManager.Instance.MuteEffect();
        }
    }

    public void SwitchValue()
    {
        m_musicSlider = GetComponent<Slider>();
        if (bgm)
        {
            int value = SoundManager.Instance.bgmMute ? 1 : 0;
            m_musicSlider.value = value;
        }
        else
        {
            int value = SoundManager.Instance.effectMute ? 1 : 0;
            m_musicSlider.value = value;
        }
    }
}
