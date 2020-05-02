using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject menu;

    public string MainMenuBgm;
    public void StartButton()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void SettingButton()
    {
        menu.GetComponent<Animator>().SetTrigger("Open");
    }

    public void SettingCloseButton()
    {
        menu.GetComponent<Animator>().SetTrigger("Close");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MusicOn(bool IsOn)
    {
        if (IsOn)
        {
            SoundManager.instance.PlayBgm(MainMenuBgm);
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
