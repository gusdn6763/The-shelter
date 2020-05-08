using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerManager player;

    public GameObject joyStick;

    [HideInInspector] public int mobCount;
    public string currenBgm;

    public bool stageClearStatus = false;
    public int currentLevel;
    public int nowStage; // 현재 플레이어가 있는 위치.

    public enum GameStaus
    {
        NONE,
        MENU,
        READY,
        START,
        PLAY,
        CLEAR,
        GAMEOVER
    }

    public GameStaus status = GameStaus.NONE;

    private void Awake()
    {
        nowStage = 0; // 0부터 시작
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
}
