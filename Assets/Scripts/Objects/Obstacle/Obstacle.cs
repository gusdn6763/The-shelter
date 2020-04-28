using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float obstacleHp; // 파괴 가능한 장애물의 체력
    public bool isDestroy; // 0이면 파괴 불가능, 1이면 파괴 가능

    void OnCollisionEnter2D(Collision2D col)
    {
        if (isDestroy)
        {
            
        }
    }
    void Destroy()
    {
        // 파괴 시 발생하는 함수
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
