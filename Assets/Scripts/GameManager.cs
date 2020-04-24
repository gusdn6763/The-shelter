using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Camera moveCamera;

    public PlayerManager player;

    public GameObject joyStick;

    public float cameraSpeed;

    [HideInInspector] public int mobCount;

    public bool stageClearStatus = false;

    public bool isOn = false;

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
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        moveCamera = Camera.main;
    }
}
