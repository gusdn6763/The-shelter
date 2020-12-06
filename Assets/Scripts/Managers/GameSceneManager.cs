using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public GameObject pausePanel;
    public JoyStick joystick;

    public SettingView setting;

    public string MoveScene;

    private void Awake()
    {
        GameManager.instance.StartScene();
    }

    private void Start()
    {
        setting.MusicOn(SoundManager.instance.bgmIsOn);
        setting.SoundOn(SoundManager.instance.soundIsOn);
    }

    public void OnClickPause()
    {
        pausePanel.SetActive(true);
        joystick.JoystickIsActive(false);
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
        joystick.JoystickIsActive(true);
    }

    public void SettingCloseButton()
    {
        setting.gameObject.SetActive(false);
        if (setting.resetToggle.isOn == true)
        {
            Time.timeScale = 1f;
            GameManager.instance.ResetInfo();
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            Time.timeScale = 0f;
            GameManager.instance.CallSave(false);
        }
    }
}
