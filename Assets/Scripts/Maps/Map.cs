﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;

[System.Serializable]
public class MapInfo // 맵, 타일에 관련된 클래스
{
    public GameObject[] ground;
    public GameObject[] walls;
    public GameObject[] exit;
    public enum e_mapTileType
    {
        EXIT, // 입구 혹은 출구
        GROUND, // 그 외 땅
        WALL, // 벽
    }
    public enum e_mapObjectType
    {
        OBJECT,
        ENEMY,
        PLAYER,
    }

    public e_mapTileType[,] mapTileArray; // 맵 타일 배열
    public e_mapObjectType[,] mapObjArray; // 오브젝트 타일 배열

    public int mapRow;
    public int mapColumns;
}
[System.Serializable]
public class MobInfo // 몹 정보
{
    public Mob mob;
    [Tooltip("어떤 위치부터 나올지(세로 기준만 끝부터)")]
    [Range(0, 8)]
    public int mobSpawnPos;
    [Tooltip("어떤 스테이지부터 나올지")]
    [Range(0, 10)]
    public int mobSpawnStage;
    [Range(0, 10)]
    public int mobMaxCount;
}
[System.Serializable]
public class ObjectInfo // 장애물 정보
{
    public int objCount;
    public GameObject objects;
}
[System.Serializable]

public class PlayerInfo // 플레이어 정보
{
    public GameObject player;
    public Transform startPoint;
}
[System.Serializable]

public class Map : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;

    [HideInInspector] public List<Mob> mobs = new List<Mob>();

    public MapInfo mapInfo;
    public MobInfo[] mobInfo;
    public ObjectInfo[] objectInfo;
    public PlayerInfo playerInfo;

    private Transform background;
    private Transform mobsParentObject;
    private Transform objParentObject;
    private Transform exitParentObject;

    public bool[,] spawnCheck;
    public int stageNum;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        mapInfo.mapTileArray = new MapInfo.e_mapTileType[mapInfo.mapRow, mapInfo.mapColumns];
        mapInfo.mapObjArray = new MapInfo.e_mapObjectType[mapInfo.mapRow, mapInfo.mapColumns];
        spawnCheck = new bool[mapInfo.mapRow + 1, mapInfo.mapColumns + 1];

        background = transform.GetChild(0);
        mobsParentObject = transform.GetChild(1);
        objParentObject = transform.GetChild(2);
        exitParentObject = transform.GetChild(3);
        transform.SetParent(this.transform);
    }
    void Start()
    {
        CreateMapTile();
        CreateColl();
        CreateMap();
        CreateMob();
        //EnhanceMob();
        CreateObject();
        if (stageNum == 0)
            MapManager.instance.StartStage(0);
    }

    private void CreateMapTile()
    {
        int exit = mapInfo.mapRow;
        for (int i = 0; i < mapInfo.mapRow; i++)
        {
            for (int j = 0; j < mapInfo.mapColumns; j++)
            {
                if (stageNum == 0 && j == 0)
                {
                    mapInfo.mapTileArray[i, j] = MapInfo.e_mapTileType.WALL;
                }
                else if (i == 0 || i == mapInfo.mapRow - 1)
                {
                    mapInfo.mapTileArray[i, j] = MapInfo.e_mapTileType.WALL;
                }
                else if (j == mapInfo.mapColumns - 1)
                {
                    mapInfo.mapTileArray[i, j] = MapInfo.e_mapTileType.EXIT;
                }
                else
                {
                    mapInfo.mapObjArray[i, j] = MapInfo.e_mapObjectType.ENEMY;
                    mapInfo.mapTileArray[i, j] = MapInfo.e_mapTileType.GROUND;
                }
            }
        }
    }

    //맵의 콜라이더 생성
    public void CreateColl()
    {
        boxCollider2D.size = new Vector2(mapInfo.mapRow, mapInfo.mapColumns);
        boxCollider2D.offset = new Vector2((((float)mapInfo.mapRow - 1) / 2), (((float)mapInfo.mapColumns - 1) / 2 - 5));
        boxCollider2D.isTrigger = true;

        //endCheck.offset = new Vector2((((float)mapInfo.mapRow - 1) / 2), boardColumns - 0.5f);
        //endCheck.size = new Vector2(mapInfo.mapRow, 1);
    }
    private void CreateMap()
    {
        GameObject tmp;

        for (int i = 0; i < mapInfo.mapRow; i++)
        {
            for (int j = 0; j < mapInfo.mapColumns; j++)
            {
                if (mapInfo.mapTileArray[i, j] == MapInfo.e_mapTileType.EXIT)
                {
                    tmp = Instantiate(mapInfo.exit[Random.Range(0, mapInfo.exit.Length)]);
                    tmp.transform.SetParent(exitParentObject);
                    tmp.transform.localPosition = new Vector3(i, j, 0f);
                    tmp.GetComponent<ExitTile>().GetInfo(stageNum);
                }
                else if (mapInfo.mapTileArray[i, j] == MapInfo.e_mapTileType.WALL)
                {
                    tmp = Instantiate(mapInfo.walls[Random.Range(0, mapInfo.walls.Length)]);
                    tmp.transform.SetParent(background);
                    tmp.transform.localPosition = new Vector3(i, j, 0f);
                }
                else //if (mapInfo.maptile[i, j] == e_mapTileType.GROUND)
                {
                    tmp = Instantiate(mapInfo.ground[Random.Range(0, mapInfo.ground.Length)]);
                    tmp.transform.SetParent(background);
                    tmp.transform.localPosition = new Vector3(i, j, 0f);
                }
            }
        }
        this.gameObject.transform.position -= new Vector3(-0.5f, -0.5f);
    }
    private void CreateMob()
    {
        for (int i = 0; i < mobInfo.Length; i++)
        {
            int mobCountTmp = Random.Range(1, mobInfo[i].mobMaxCount + GameManager.instance.currentLevel/3 + 1);

            if (MapManager.instance.maps.Length < mobInfo[i].mobSpawnStage)
                continue;

            for (int j = 0; j < mobCountTmp; j++)
            {
                int l = Random.Range(1, mapInfo.mapRow - 1);
                int k = Random.Range(mapInfo.mapColumns - MapManager.instance.col + mobInfo[i].mobSpawnPos - 1, mapInfo.mapColumns - 2);

                if (spawnCheck[l, k] == true || spawnCheck[l, k -1] == true)
                {
                    j--;
                    continue;
                }

                Mob mobTmp = Instantiate(mobInfo[i].mob);
                mobTmp.transform.SetParent(mobsParentObject);
                mobTmp.transform.localPosition = new Vector3(l, k);
                spawnCheck[l, k] = true;
                mobTmp.gameObject.SetActive(false);

                mobTmp.target = GameManager.instance.player.gameObject;
                mobTmp.agent.map = GetComponent<PolyNav2D>();

                mobs.Add(mobTmp.GetComponent<Mob>());
            }
        }
    }
    private void CreateObject()
    {
        for (int i = 0; i < objectInfo.Length; i++)
        {
            int CountTmp = objectInfo[i].objCount;

            for (int j = 0; j < CountTmp; j++)
            {
                int l = Random.Range(2, 6);
                int k = Random.Range(2, mapInfo.mapColumns - 2);

                if (spawnCheck[l, k] == true)
                {
                    j--;
                    continue;
                }
                GameObject objects = Instantiate(objectInfo[i].objects);
                objects.transform.SetParent(background);
                objects.transform.localPosition = new Vector3(l, k);
                spawnCheck[l, k] = true;
            }
        }
    }

/*
    public void EnhanceMob()
    {
        for (int i = 0; i < mobs.Count; i++)
        {
            mobs[i].currentHp += GameManager.instance.currentLevel * 10;
            mobs[i].HP += GameManager.instance.currentLevel * 10;
            mobs[i].currentHp += GameManager.instance.currentLevel * 10;
            mobs[i].Speed += GameManager.instance.currentLevel * 0.1f;
        }
    }
    */
}