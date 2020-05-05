using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public GameObject chooseStage;
    public GameObject button;

    public Setting menu;

    public string MainMenuBgm;

    private void Awake()
    {
        GameManager.instance.currenBgm = MainMenuBgm;
        menu.MusicOn(SoundManager.instance.bgmIsOn);
        menu.SoundOn(SoundManager.instance.soundIsOn);
    }

    public void StartButton()
    {
        button.SetActive(false);
        chooseStage.SetActive(true);
        SceneManager.LoadScene("LoadingScene");
    }

    public void SettingButton()
    {
        menu.Open();
    }

    public void SettingCloseButton()
    {
        menu.Close();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
