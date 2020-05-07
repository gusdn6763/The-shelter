using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerManager player;

    public GameObject firstPopupView;

    [HideInInspector]public string currenBgm;
    public string menuBgm;
    public string gameBgm;

    public bool stageClearStatus = false;
    public int mobCount;
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
    }
}
