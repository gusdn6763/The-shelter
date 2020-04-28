using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    public Map[] maps;
    public Map map;

    private int col = 10;
    private int row = 7;
    public int maps_count;
    public int map_random_add;

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
        maps = new Map[maps_count];
    }

    void Start()
    {
        CreateStage();
    }

    public void CreateStage()
    {
        float columns = 0;

        map.mapInfo.mapRow = row;
        for (int i = 0; i < maps_count; i++)
        {
            map.mapInfo.mapColumns = Random.Range(col, col + Random.Range(0, map_random_add));
            Map tmp = Instantiate(map, new Vector3(-0.5f * map.mapInfo.mapRow, (float)(columns), 0),Quaternion.identity);
            tmp.transform.localPosition = new Vector3(-0.5f * map.mapInfo.mapRow, (float)(columns), 0);
            columns += map.mapInfo.mapColumns;
            tmp.transform.SetParent(this.transform);
            maps[i] = tmp;
        }
    }
}
