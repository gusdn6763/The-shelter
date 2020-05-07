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
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerManager player;

    public GameObject firstPopupView;

    public Data data;

    [HideInInspector]public string currenBgm;
    public string menuBgm;
    public string gameBgm;

    public bool stageClearStatus = false;
    public int mobCount;
    public int PlayerClearStage;
    public int currentLevel;
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
        player = FindObjectOfType<PlayerManager>();
    }

    private void Start()
    {
        CallLoad(false);
    }

    public void MenuScene()
    {
        player.gameObject.SetActive(false);
        currenBgm = menuBgm;
        SoundManager.instance.PlayBgm(currenBgm);
    }

    public void StartScene()
    {
        player.gameObject.SetActive(true);
        currenBgm = gameBgm;
        SoundManager.instance.PlayBgm(currenBgm);

        if (PlayerPrefs.GetInt(Constant.kFirstIntroduceView, 1) == 1)
        {
            Time.timeScale = 0f;
            Instantiate(firstPopupView);
            PlayerPrefs.SetInt(Constant.kFirstIntroduceView, 0);
        }
        MapManager.instance.CreateStage(currentLevel);
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
            }
            PlayerClearStage = data.playerClearStage;
            Debug.Log("불러오기성공");
            Debug.Log(SoundManager.instance.audioSourceBgm.volume);
        }
        else
        {
            Debug.Log("저장된 세이브 파일이 없습니다");
        }
        file.Close();
    }
}
