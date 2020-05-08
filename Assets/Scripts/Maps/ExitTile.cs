using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTile : MonoBehaviour
{
    public int exitNum;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            int stage = GameManager.instance.nowStage;
            if (MapManager.instance.maps.Length > stage + 1)
            {
                if (MapManager.instance.maps[stage].MobsCount == 0)
                {
                    this.gameObject.GetComponent<Collider2D>().isTrigger = true;
                    if (stage == exitNum) // 앞으로 전진
                    {
                        GameManager.instance.nowStage++;
                        GameManager.instance.player.Move(2.0f, 2.0f);
                        Invoke("InvokeCollsionEnter2DFront", 2.0f);
                    }
                    else // 뒤로 후진
                    {
                        //MapManager.instance.MoveStage(exitNum);
                        GameManager.instance.nowStage--;
                        GameManager.instance.player.Move(-2.0f, 2.0f);
                        Invoke("InvokeCollsionEnter2DBack", 2.0f);
                    }
                    // 플레이어 이동
                    // 몹 비활성화
                    // 몹 활성화
                    // 카메라 이동
                }
                else
                {
                    Debug.Log("ss");
                }
            } else if (MapManager.instance.maps.Length == stage + 1) // 마지막 스테이지일 시
            {
                if (MapManager.instance.maps[stage].MobsCount == 0)
               {
                    this.gameObject.GetComponent<Collider2D>().isTrigger = true;
                    if (stage == exitNum) // 게임 종료 메세지 and 메뉴로
                    {
                        DatabaseManager.instance.currentPlayerClearStage++;
                        GameManager.instance.nowStage = 0;
                        SceneManager.LoadScene("StartScreen");
                        //게임종료
                        // 메뉴로
                    }
                    else // 뒤로 후진
                    {
                        //MapManager.instance.MoveStage(exitNum);
                        GameManager.instance.nowStage--;
                        GameManager.instance.player.MoveBack(2.0f, 2.0f);
                        Invoke("InvokeCollsionEnter2DBack", 2.0f);
                    }
                    // 플레이어 이동
                    // 몹 비활성화
                    // 몹 활성화
                    // 카메라 이동
                }
            }
        }
    }

    void InvokeCollsionEnter2DFront()
    {
        MapManager.instance.StartStage(exitNum + 1);
        this.gameObject.GetComponent<Collider2D>().isTrigger = false;
    }
    void InvokeCollsionEnter2DBack()
    {
        MapManager.instance.StartStage(exitNum);
        this.gameObject.GetComponent<Collider2D>().isTrigger = false;   
    }
}
