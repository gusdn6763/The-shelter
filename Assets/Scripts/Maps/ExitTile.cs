using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTile : MonoBehaviour
{
    public Collider2D collider2d;
    public int exitNum;

    private void Awake()
    {
        collider2d = GetComponent<Collider2D>();
    }
    public void GetInfo(int num)
    {
        exitNum = num;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            int stage = GameManager.instance.nowStage;
            if (MapManager.instance.maps[stage].mobs.Count == 0)
            {
                if (MapManager.instance.maps.Length > stage + 1)
                {
                    collider2d.isTrigger = true;
                    if (stage == exitNum) // 앞으로 전진
                    {
                        GameManager.instance.nowStage++;
                        GameManager.instance.player.Move(2.0f, 2.0f);
                        Invoke("InvokeCollsionEnter2DFront", 2.0f);
                    }
                    else // 뒤로 후진
                    {
                        GameManager.instance.nowStage--;
                        GameManager.instance.player.Move(-2.0f, 2.0f);
                        Invoke("InvokeCollsionEnter2DBack", 2.0f);
                    }
                }
                else if (MapManager.instance.maps.Length == stage + 1) // 마지막 스테이지일 시
                {
                    this.gameObject.GetComponent<Collider2D>().isTrigger = true;
                    if (stage == exitNum) // 게임 종료 메세지 and 메뉴로
                    {
                        Debug.Log("123");
                        GameManager.instance.ClearStage();
                    }
                    else // 뒤로 후진
                    {
                        //MapManager.instance.MoveStage(exitNum);
                        GameManager.instance.nowStage--;
                        GameManager.instance.player.Move(-2.0f, 2.0f);
                        Invoke("InvokeCollsionEnter2DBack", 2.0f);
                    }
                }
            }
        }
    }

    void InvokeCollsionEnter2DFront()
    {
        MapManager.instance.StartStage(exitNum + 1);
        collider2d.isTrigger = false;
    }
    void InvokeCollsionEnter2DBack()
    {
        MapManager.instance.StartStage(exitNum);
        collider2d.isTrigger = false;   
    }
}
