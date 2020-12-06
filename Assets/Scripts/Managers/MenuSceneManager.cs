using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    public GameObject menuButton;
    public GameObject chooseStage;

    public SettingView setting;

    private void Start()
    {
        GameManager.instance.MenuScene();
        setting.MusicOn(SoundManager.instance.bgmIsOn);
        setting.SoundOn(SoundManager.instance.soundIsOn);
    }

    public void StartButton()
    {
        menuButton.SetActive(false);
        chooseStage.SetActive(true);
    }

    public void StartCancleButton()
    {
        menuButton.SetActive(true);
        chooseStage.SetActive(false);
    }

    public void SettingButton()
    {
        setting.Open();
    }

    //설정창을 닫을시 시작하며, 초기화 유무를 확인하는 함수
    public void SettingCloseButton()
    {
        setting.Close();
        if (setting.resetToggle.isOn == true)
        {
            GameManager.instance.ResetInfo();
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            GameManager.instance.CallSave(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    void OnApplicationQuit()
    {
        Application.Quit();
#if !UNITY_EDITOR
        System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
    }

    //스테이지를 선택하는 버튼을 클릭시 시작하는 함수
    public void ImprovementStage(int stageCount)
    {
        GameManager.instance.currentLevel = stageCount;
        //로딩창으로 넘어가며 GameSceneUI.cs의 Awake함수 시작
        SceneManager.LoadScene("LoadingScene");
    }
}
