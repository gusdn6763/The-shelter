using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneUI : MonoBehaviour
{
    public GameObject pausePanel;

    public Setting setting;

    public string MoveScene;

    private void Awake()
    {
        GameManager.instance.StartScene();
        setting.MusicOn(SoundManager.instance.bgmIsOn);
        setting.SoundOn(SoundManager.instance.soundIsOn);
    }
    public void OnClickPause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnClickMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MoveScene);
    }

    public void OnClickSetting()
    {
        setting.gameObject.SetActive(true);
    }

    public void OnClickCancle()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
