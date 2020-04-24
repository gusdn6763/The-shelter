using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [Range(0, 10)]
    public int mobSpawnPos;
    [Tooltip("어떤 스테이지부터 나올지")]
    [Range(0, 5)]
    public int mobSpawnStage;
    [Range(0, 10)]
    public int mobMaxCount;
}
[System.Serializable]
public class ObjectInfo // 장애물 정보
{
    public int objCount;
    public GameObject[] objects;
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

    public MapInfo mapInfo;
    public MobInfo[] mobInfo;
    public ObjectInfo objectInfo;
    public PlayerInfo playerInfo;

    private Transform background;
    private Transform mobsParentObject;
    private Transform objParentObject;
    private Transform exitParentObject;

    public bool[,] spawnCheck;

    void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        mapInfo.mapTileArray = new MapInfo.e_mapTileType[mapInfo.mapRow, mapInfo.mapColumns];
        mapInfo.mapObjArray = new MapInfo.e_mapObjectType[mapInfo.mapRow, mapInfo.mapColumns];

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
        CreateObject();
    }

    private void CreateMapTile()
    {
        int exit = mapInfo.mapRow;
        for (int i = 0; i < mapInfo.mapRow; i++)
        {
            for (int j = 0; j < mapInfo.mapColumns; j++)
            {
                if (i == 0 || i == mapInfo.mapRow - 1 || j == mapInfo.mapColumns - 1 && i != exit)
                {
                    mapInfo.mapTileArray[i, j] = MapInfo.e_mapTileType.WALL;
                }
                else if (j == mapInfo.mapColumns - 1 && i == exit)
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
        GameObject temp;
        int pool_index = 0;

        for (int i = 0; i < mobInfo.Length; i++)
        {
            for (int j = 0; j < mapInfo.mapRow; j++)
            {
                for (int k = 0; k < mapInfo.mapColumns; k++)
                {
                    if (mapInfo.mapObjArray[k, k] == MapInfo.e_mapObjectType.ENEMY)
                    {
                        if (pool_index < mobInfo[k].mobMaxCount)
                        {
                            temp = mobInfo[i].mob.gameObject;
                            temp.transform.SetParent(background);
                            temp.transform.localPosition = new Vector3(Random.Range((float)k, (float)k + 1), Random.Range((float)k, (float)k + 1), 0f);
                            spawnCheck
                        }
                        pool_index++;
                    }
                }
            }
        }
    }
    private void CreateObject()
    {
        GameObject[] pool = PoolingObj(objectInfo.objects, objectInfo.objCount);
        GameObject temp;
        int pool_index = 0;

        for (int i = 0; i < mapInfo.mapRow; i++)
        {
            for (int j = 0; j < mapInfo.mapColumns; j++)
            {
                if (mapInfo.mapObjArray[i, j] == MapInfo.e_mapObjectType.OBJECT)
                {
                    if (pool_index < objectInfo.objCount)
                    {
                        temp = Instantiate(pool[pool_index]);
                        temp.transform.SetParent(background);
                        temp.transform.localPosition = new Vector3(Random.Range((float)i, (float)i + 1), Random.Range((float)j, (float)j + 1), 0f);
                    }
                    pool_index++;
                }
            }
        }
    }
    private T[] PoolingObj<T>(T[] pool, int count)
    {
        T[] list = new T[count];
        for (int i = 0; i < count; i++)
        {
            list[i] = pool[Random.Range(0, pool.Length)];
        }
        return list;
    }
}