using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private Map[] maps;
    public int maps_count = 1;
    private int col = 10;
    private int row = 7;
    public Map map;

    private void SettingMap()
    {
        map.mapInfo.mapColumns = Random.Range(col - 6, col);
        map.mapInfo.mapRow = row;
    }
    public void CreateStage()
    {
        float columns = 0;
        for (int i = 0; i < maps_count; i++)
        {
            SettingMap();
            //map.mapInfo.mapTileArray = new MapInfo.e_mapTileType[map.mapInfo.mapRow, map.mapInfo.mapColumns];
            Map tmp = Instantiate(map, new Vector3(-0.5f * map.mapInfo.mapRow, (float)(columns), 0),Quaternion.identity);
            Debug.Log(map.mapInfo.mapColumns);
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

    void Update()
    {
        
    }
}
