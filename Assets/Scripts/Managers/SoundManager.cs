using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // 곡의 이름.
    public AudioClip clip; // 곡.
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceBgm;

    public string[] playSoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    public bool soundIsOn = true;
    public bool bgmIsOn = true;

    void Awake() // 객체 생성시 최초 실행.
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        audioSourceEffects = GetComponentsInChildren<AudioSource>();
    }
    void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];
        if (bgmIsOn)
        {
            audioSourceBgm.Play();
        }
    }

    public void PlaySE(string _name)
    {
        if (soundIsOn)
        {
            for (int i = 0; i < effectSounds.Length; i++)
            {
                if (_name == effectSounds[i].name)
                {
                    for (int j = 0; j < audioSourceEffects.Length; j++)
                    {
                        if (!audioSourceEffects[j].isPlaying)
                        {
                            playSoundName[j] = effectSounds[i].name;
                            audioSourceEffects[j].clip = effectSounds[i].clip;
                            audioSourceEffects[j].Play();
                            return;
                        }
                    }
                    Debug.Log("모든 가용 AudioSource가 사용중입니다.");
                    return;
                }
            }
            Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
        }
    }
    public void PlaySEVolume(float volume)
    {
        for (int i = 0; i < playSoundName.Length - 1; i++)
        {
            audioSourceEffects[i].volume = volume;
        }
    }
    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                return;
            }
        }
        Debug.Log("재생 중인" + _name + "사운드가 없습니다.");
    }
    public void CheckSound(bool isOn)
    {
        soundIsOn = isOn;
        if (!soundIsOn)
        {
            for (int i = 0; i < audioSourceEffects.Length - 1; i++)
            {
                audioSourceEffects[i].Stop();
            }
        }
    }

    public void PlayBgm(string _name)
    {
        if (bgmIsOn)
        {
            for (int i = 0; i < bgmSounds.Length; i++)
            {
                if (_name == bgmSounds[i].name)
                {
                    audioSourceBgm.Stop();
                    audioSourceBgm.clip = bgmSounds[i].clip;
                    audioSourceBgm.Play();
                    return;
                }
            }
        }
    }

    public void StopBgm()
    {
        audioSourceBgm.Stop();
    }

    public void PlayBgmVolume(float volume)
    {
        audioSourceBgm.volume = volume;
    }
}
