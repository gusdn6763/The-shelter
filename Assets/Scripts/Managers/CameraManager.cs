using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public GameObject target;
    internal Transform trans;

    private Vector3 targetPosition;
    public float moveSpeed = 1f;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        trans = GetComponent<Transform>();
    }

    void Update()
    {
        if (target.gameObject != null)
        {
            Vector3 mapPosition = MapManager.instance.startPoint[GameManager.instance.player.nowStage];
            int mapRow = MapManager.instance.maps[GameManager.instance.player.nowStage].mapInfo.mapRow;
            int mapCol = MapManager.instance.maps[GameManager.instance.player.nowStage].mapInfo.mapColumns;
            targetPosition.Set(target.transform.position.x, target.transform.position.y, - 10f);
            targetPosition.x = Mathf.Clamp(target.transform.position.x, mapPosition.x + mapRow / 2 - 3 + 3.5f, mapPosition.x - mapRow / 2 + 3 + 3.5f); // 임의의 값, 3.5f는 Map이 -3.5f 좌표값을 갖기때문에 더해줌
            targetPosition.y = Mathf.Clamp(target.transform.position.y, mapPosition.y, mapPosition.y + mapCol - 10); // 10 카메라 길이
            trans.position = Vector3.Lerp(trans.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

/*
    Vector3 BoundCameraLocation(Vector3 position)
    {
        Vector3 cameraLocation = position;
        //MapManager.instance.maps[target.GetComponent<PlayerManager>().nowStage].mapInfo.mapRow;
        //x = 

        if (cameraLocation.x > x) cameraLocation.x = x;
        if (cameraLocation.x < x2) cameraLocation.x = x2;
        if (cameraLocation.y > y) cameraLocation.y = y;
        if (cameraLocation.y < y) cameraLocation.y = y;

        return (cameraLocation);
    }
*/
}
