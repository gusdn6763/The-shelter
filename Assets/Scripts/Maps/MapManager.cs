using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private Map[] maps;
    public int maps_count;
    private int col = 10;
    private int row = 7;
    public Map map;

    private void SettingMap()
    {
        map.mapInfo.mapColumns = Random.Range(col , col + Random.Range(0,6));
        map.mapInfo.mapRow = row;
    }
    public void CreateStage()
    {
        float columns = 0;
        for (int i = 0; i < maps_count; i++)
        {
            SettingMap();
            Map tmp = Instantiate(map, new Vector3(-0.5f * map.mapInfo.mapRow, (float)(columns), 0),Quaternion.identity);
            columns += map.mapInfo.mapColumns;
            tmp.transform.SetParent(this.transform);
            maps[i] = tmp;
        }
    }
    public void Awake()
    {
        maps = new Map[maps_count];
    }

    void Start()
    {
        CreateStage();
    }
}
