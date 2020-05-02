using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTile : MonoBehaviour
{
    public int exitNum;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        { // 몹 파괴 시 List에서 제거된다면, MobsCount의 구현을 List.Count로 변경
            int stage = col.gameObject.GetComponent<PlayerManager>().nowStage;
            if (MapManager.instance.maps.Length > stage)
            {
                if (MapManager.instance.maps[stage].MobsCount == 0)
                {
                    this.gameObject.GetComponent<Collider2D>().isTrigger = true;
                    if (GameManager.instance.player.nowStage == exitNum) // 앞으로 전진
                    {
                        GameManager.instance.player.nowStage++;
                        GameManager.instance.player.Move(2.0f, 2.0f);
                        Invoke("InvokeCollsionEnter2DFront", 2.0f);
                    }
                    else // 뒤로 후진
                    {
                        //MapManager.instance.MoveStage(exitNum);
                        GameManager.instance.player.nowStage--;
                        GameManager.instance.player.MoveBack(2.0f, 2.0f);
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
            }
        }
    }

    void InvokeCollsionEnter2DFront()
    {
        MapManager.instance.MoveStage(exitNum + 1);
        this.gameObject.GetComponent<Collider2D>().isTrigger = false;
    }
    void InvokeCollsionEnter2DBack()
    {
        MapManager.instance.MoveStage(exitNum);
        this.gameObject.GetComponent<Collider2D>().isTrigger = false;   
    }
}
