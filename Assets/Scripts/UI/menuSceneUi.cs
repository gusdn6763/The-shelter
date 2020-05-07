﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuSceneUi : MonoBehaviour
{
    public GameObject button;
    public GameObject chooseStage;

    public Setting setting;

    private void Awake()
    {
        GameManager.instance.MenuScene();
        setting.MusicOn(SoundManager.instance.bgmIsOn);
        setting.SoundOn(SoundManager.instance.soundIsOn);
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
        setting.Open();
    }

    public void SettingCloseButton()
    {
        setting.Close();
    }

    public void LoadData()
    {
        PlayerPrefs.GetInt(Constant.kLoadLevel, DatabaseManager.instance.currentPlayerClearStage);
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
