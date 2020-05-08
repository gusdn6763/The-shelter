using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameManager gameManager;
    public MapManager mapManager;

    public GameObject target;
    internal Transform trans;

    private Vector3 targetPosition;
    public float moveSpeed = 1f;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        mapManager = FindObjectOfType<MapManager>();
        trans = GetComponent<Transform>();
        target = GameManager.instance.player.gameObject;
    }

    void Update()
    {
        if (target.gameObject != null)
        {
            Vector3 mapPosition = mapManager.startPoint[gameManager.nowStage];
            int mapRow = mapManager.maps[gameManager.nowStage].mapInfo.mapRow;
            int mapCol = mapManager.maps[gameManager.nowStage].mapInfo.mapColumns;
            targetPosition.Set(target.transform.position.x, target.transform.position.y, - 10f);
            targetPosition.x = Mathf.Clamp(target.transform.position.x, mapPosition.x + mapRow / 2 - 3 + 3.5f, mapPosition.x - mapRow / 2 + 3 + 3.5f); // 임의의 값, 3.5f는 Map이 -3.5f 좌표값을 갖기때문에 더해줌
            targetPosition.y = Mathf.Clamp(target.transform.position.y, mapPosition.y, mapPosition.y + mapCol - 10); // 10 카메라 길이
            trans.position = Vector3.Lerp(trans.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
