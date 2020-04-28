using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTile : MonoBehaviour
{
    public int exitNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        { // 몹 파괴 시 List에서 제거된다면, MobsCount의 구현을 List.Count로 변경
            int stage = col.gameObject.GetComponent<PlayerManager>().nowStage;
            if (MapManager.instance.maps.Length > stage)
            {
                if (MapManager.instance.maps[stage].MobsCount == 0)
                {
                    Debug.Log("tets");
                    this.gameObject.GetComponent<Collider2D>().isTrigger = true;
                    if (GameManager.instance.player.nowStage == exitNum)
                    {
                        MapManager.instance.MoveStage(exitNum + 1);
                        GameManager.instance.player.nowStage++;
                    }
                    else
                    {
                        MapManager.instance.MoveStage(exitNum);
                        GameManager.instance.player.nowStage--;
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
}
