using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : ViewManager
{
    public Toggle musicOn;
    public Toggle soundOn;
    public Slider musicVolume;
    public Slider soundVolume;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        musicOn.isOn = SoundManager.instance.bgmIsOn;
        soundOn.isOn = SoundManager.instance.soundIsOn;
        musicVolume.value = SoundManager.instance.audioSourceBgm.volume;
        soundVolume.value = SoundManager.instance.audioSourceEffects[0].volume;
    }

    public void MusicOn(bool IsOn)
    {
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
