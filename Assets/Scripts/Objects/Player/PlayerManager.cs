﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{
    public enum CharacterStatus
    {
        NONE,
        IDLE,
        MOVE,
        ATTACK,
        DIE
    }

    public CharacterStatus playerStatus;                

    public override void Awake()
    {
        base.Awake();
    }

    public void Update()
    {
        switch (playerStatus)
        {
            case CharacterStatus.NONE:
                break;
            case CharacterStatus.IDLE:
                    //방안에 몹이 있을시
                    playerStatus = CharacterStatus.ATTACK;
                animator.SetBool("Move", false);
                break;
            case CharacterStatus.MOVE:
                transform.Translate((Vector3.down * (speed * 0.1f)) * Time.deltaTime);
                animator.SetBool("Move", true);
                break;
            case CharacterStatus.ATTACK:
                //ShowTarget();
                //순찰 및 추적을 정지
                //TODO:
                    fireCtrl.isFire = true;
                    if (!fireCtrl.isReload && fireCtrl.isFire)
                    {
                        if (Time.time >= fireCtrl.nextFire)        //현재 시간이 다음 발사 시간보다 큰지를 확인
                        {
                            animator.SetTrigger("Attack_Rifle");
                            fireCtrl.nextFire = Time.time + fireCtrl.fireRate + Random.Range(0.0f, 0.3f); //다음 발사 시간 계산
                        }
                    }
                break;
            case CharacterStatus.DIE:
                break;
        }
    }

    //나중에 처음 시작할때 자동이로 움직여주는 함수
    public void Move(float distance,float startTime)        
    {
        StartCoroutine(MoveCoroutine(distance, startTime));
    }

    IEnumerator MoveCoroutine(float distance,float startTime)
    {
        Vector3 toPos = new Vector3(transform.position.x, transform.position.y+distance, transform.position.z);
        while (startTime > 0)
        {
            startTime -= Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, toPos,(startTime+(Time.deltaTime/startTime))-startTime);
            yield return null;
        }
        playerStatus = CharacterStatus.IDLE;
    }
}