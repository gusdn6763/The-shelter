using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingView : ViewManager
{
    public Toggle resetToggle;
    public Toggle musicOn;
    public Toggle soundOn;
    public Slider musicVolume;
    public Slider soundVolume;

    private void OnEnable()
    {
        musicOn.isOn = SoundManager.instance.bgmIsOn;
        soundOn.isOn = SoundManager.instance.soundIsOn;
        musicVolume.value = SoundManager.instance.audioSourceBgm.volume;
        soundVolume.value = SoundManager.instance.audioSourceEffects[0].volume;
    }

    public void MusicOn(bool IsOn)
    {
        SoundManager.instance.bgmIsOn = IsOn;
        if (IsOn)
        {
            SoundManager.instance.PlayBgm(GameManager.instance.currenBgm);
        }
        else
        {
            SoundManager.instance.StopBgm();
        }
    }
    public void MusicValue(float value)
    {
        SoundManager.instance.PlayBgmVolume(value);
    }
    public void SoundOn(bool IsOn)
    {
        SoundManager.instance.CheckSound(IsOn);
    }
    public void PlaySEVolume(float value)
    {
        SoundManager.instance.PlaySEVolume(value);
    }
}
