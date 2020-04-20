using System.Collections;
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

    public CharacterStatus playerStatus;                /// joystick 스크립트로 변경

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
                    attack_Delay_Tmp = attack_Delay;
                    playerStatus = CharacterStatus.ATTACK;
                animator.SetBool("Move", false);
                break;
            case CharacterStatus.MOVE:
                transform.Translate((Vector3.down * (speed * 0.1f)) * Time.deltaTime);
                animator.SetBool("Move", true);
                break;
            case CharacterStatus.ATTACK:
                ShowTarget();
                if (attack_Delay >= 0)
                    attack_Delay_Tmp -= Time.deltaTime;
                else
                {
                    attack_Delay_Tmp = attack_Delay;
                    animator.SetTrigger("Attack_Rifle");
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
