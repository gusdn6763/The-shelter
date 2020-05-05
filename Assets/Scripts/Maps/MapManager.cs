using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    public Map[] maps;
    public Map map;

    public int col = 11;
    public int row = 7;
    public int maps_count;
    public int map_random_add;
    private int activateStage; // 어떤 스테이지가 Activate 되어있는가? 사용하지않음.
    public List<Vector3> startPoint = new List<Vector3>(); // 각 맵의 시작 위치
    public void Awake()
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
        maps = new Map[maps_count];
        CreateStage();
    }

    public void StartStage(int stageNum) // from.maps
    {
        for (int i = 0; i < maps[stageNum].MobsCount; i++) // 
        {
            maps[stageNum].mobs[i].gameObject.SetActive(true);
        }
    }

    public void CreateStage()
    {
        float columns = 0;

        map.mapInfo.mapRow = row;
        for (int i = 0; i < maps_count; i++)
        {
            map.mapInfo.mapColumns = Random.Range(col, col + map_random_add);
            Map tmp = Instantiate(map, new Vector3(-0.5f * map.mapInfo.mapRow, (float)(columns), 0),Quaternion.identity);
            tmp.transform.position = new Vector3(-0.5f * map.mapInfo.mapRow, (float)(columns), 0);
            startPoint.Add(tmp.transform.localPosition);
            columns += map.mapInfo.mapColumns;
            tmp.stageNum = i;
            tmp.transform.SetParent(this.transform);
            maps[i] = tmp;
        }
    }

    public void MoveStage(int stageNum)
    {
        StartStage(stageNum);
        //GameManager.instance.player.nowStage
    }
}
