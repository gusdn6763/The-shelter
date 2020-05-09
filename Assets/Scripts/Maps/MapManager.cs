using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    [HideInInspector]public Map[] maps;
    public GameObject shleter;
    public Map map;

    [HideInInspector]public Vector3[] startPoint; // 각 맵의 시작 위치

    public int col = 11;
    public int row = 7;

    public int maps_count;
    public int map_random_add; //세로길이를 얼마나 늘릴지
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void CreateStage(int currentLevel)
    {
        float columns = 0;

        map.mapInfo.mapRow = row;
        maps_count = currentLevel / 2 + 1;
        maps = new Map[maps_count];
        startPoint = new Vector3[maps_count];
        for (int i = 0; i < maps_count; i++)
        {
            map.mapInfo.mapColumns = Random.Range(col, col + map_random_add);
            Map tmp = Instantiate(map, new Vector3(-0.5f * map.mapInfo.mapRow, (float)(columns), 0),Quaternion.identity);
            tmp.transform.position = new Vector3(-0.5f * map.mapInfo.mapRow, (float)(columns), 0);
            tmp.transform.SetParent(this.transform);
            tmp.stageNum = i;

            columns += map.mapInfo.mapColumns;
            startPoint[i] = tmp.transform.localPosition;
            maps[i] = tmp;

            if (i + 1 == maps_count)
            {
                GameObject theShleter = Instantiate(shleter, transform);
                theShleter.transform.position = new Vector3(0, columns - 4.5f, 0);
            }
        }
    }

    public void StartStage(int stageNum) // from.maps
    {
        for (int i = 0; i < maps[stageNum].mobs.Count; i++) // 
        {
            maps[stageNum].mobs[i].gameObject.SetActive(true);
            GameManager.instance.player.mobs = maps[stageNum].mobs;
        }
    }
}
