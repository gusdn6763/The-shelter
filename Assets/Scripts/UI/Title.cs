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

    private void Start()
    {
        chooseStage.SetActive(false);
    }

    public void StartButton()
    {
        button.SetActive(false);
        chooseStage.SetActive(true);
    }

    public void StartCancleButton()
    {
        button.SetActive(true);
        chooseStage.SetActive(false);
    }

    public void SettingButton()
    {
        menu.Open();
    }

    public void SettingCloseButton()
    {
        menu.Close();
    }

    public void LoadData()
    {
        PlayerPrefs.GetInt("Level", DatabaseManager.instance.currentPlayerClearStage);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ImprovementStage(int stageCount)
    {
        GameManager.instance.currentLevel = stageCount;
        SceneManager.LoadScene("LoadingScene");
    }
}
