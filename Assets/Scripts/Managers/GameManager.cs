using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Data
{
    public bool bgmOn;
    public bool soundOn;

    public float bgmVolume;
    public float soundVolume;

    public int playerClearStage;
    public int playerMoney;
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerManager player;

    public GameObject firstPopupView;
    public GameObject victoryView;
    public GameObject failedView;

    [HideInInspector]public Data data;

    [HideInInspector]public string currenBgm;
    public string menuBgm;
    public string gameBgm;
    public string win;

    public int PlayerClearStage = 1;         //플레이어가 클리어한 스테이지
    public int currentLevel;                                    //플레이어가 선택한 스테이지
    public int nowStage; // 현재 플레이어가 있는 위치.

    private void Awake()
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
    }

    public void MenuScene()
    {
        player.gameObject.SetActive(false);
        currenBgm = menuBgm;
        SoundManager.instance.PlayBgm(currenBgm);
        PlayerReset();
        CallLoad(true);
    }

    public void  PlayerReset()
    {
        player.HP = 100f;
        player.currentHp = 100f;
        player.Armor = 0;
        player.GetComponent<FireCtrl>().remainingBullet = 10;
        player.transform.position = new Vector3(0, -4f, 0);
    }

    public void StartScene()
    {
        Vector3 startPos = new Vector3(0f, -4f, 0f);

        player.gameObject.SetActive(true);
        player.transform.position = startPos;

        currenBgm = gameBgm;
        SoundManager.instance.PlayBgm(currenBgm);

        if (PlayerPrefs.GetInt("FirstView", 1) == 1)
        {
            Time.timeScale = 0f;
            Instantiate(firstPopupView);
            PlayerPrefs.SetInt("FirstView", 0);
        }
        MapManager.instance.CreateStage(currentLevel);
    }

    public void ClearStage()
    {
        if(currentLevel == PlayerClearStage)
        {
            PlayerClearStage++;
        }
        player.gameObject.SetActive(false);

        VictoryView victoryViewPanel = Instantiate(victoryView).GetComponent<VictoryView>();
        victoryViewPanel.GetInfo(player.currentMoney,3,PlayerClearStage - 1);
        SoundManager.instance.PlaySE(win);
        player.currentMoney += currentLevel * 10;
        CallSave(true);
    }

    public void FailedStage()
    {
        Instantiate(failedView);
        Time.timeScale = 0f;
        PlayerReset();
        CallLoad(true);
    }

    public void CallSave(bool stageClear)
    {
        data.bgmOn = SoundManager.instance.bgmIsOn;
        data.soundOn = SoundManager.instance.soundIsOn;
        data.bgmVolume = SoundManager.instance.audioSourceBgm.volume;
        data.soundVolume = SoundManager.instance.audioSourceEffects[0].volume;

        if (stageClear)
        {
            data.playerClearStage = PlayerClearStage;
            data.playerMoney = player.currentMoney;
        }

        Debug.Log("기초 데이터 성공");

        BinaryFormatter bf = new BinaryFormatter();                             //파일 변환
        FileStream file = File.Create(Application.dataPath + "/SaveFile.dat");  //파일 입출력

        bf.Serialize(file, data);
        file.Close();

        Debug.Log(Application.dataPath + "의 위치에 저장했습니다.");
    }

    public void CallLoad(bool stageClear)
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (!(File.Exists(Application.dataPath + "/SaveFile.dat")))
        {
            Debug.Log("없음");
            return;
        }

        FileStream file = File.Open(Application.dataPath + "/SaveFile.dat", FileMode.Open);

        if (file != null && file.Length > 0)
        {
            data = (Data)bf.Deserialize(file);

            SoundManager.instance.bgmIsOn = data.bgmOn;
            SoundManager.instance.soundIsOn = data.soundOn;
            SoundManager.instance.audioSourceBgm.volume = data.bgmVolume;
            SoundManager.instance.audioSourceEffects[0].volume = data.soundVolume;
            if (stageClear)
            {
                PlayerClearStage = data.playerClearStage;
                player.currentMoney = data.playerMoney;
            }
            Debug.Log("불러오기성공");
        }
        else
        {
            Debug.Log("저장된 세이브 파일이 없습니다");
        }
        file.Close();
    }

    public void ResetInfo()
    {
        SoundManager.instance.bgmIsOn = true;
        SoundManager.instance.soundIsOn = true;
        SoundManager.instance.audioSourceBgm.volume = 1f;
        SoundManager.instance.audioSourceEffects[0].volume = 1f;
        PlayerPrefs.SetInt("FirstView", 1);
        PlayerClearStage = 1;
        player.currentMoney = 0;
        PlayerReset();
        CallSave(true);
    }
}
