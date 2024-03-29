﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : Mob
{
    public override IEnumerator StartStatus()
    {
        while (!isDie)
        {
            if (Time.timeScale != 1f)
            {
                fireCtrl.nextFire = Time.time + waitTime;
            }
            if (enemyStatus == CharacterStatus.DIE) yield break;

            if (fireCtrl.isReload)
            {
                enemyStatus = CharacterStatus.AVODING;
            }

            else if (FindPlayer())
            {
                if(CollEtcObject())
                {
                    if (Comparison())
                    {
                        enemyStatus = CharacterStatus.ATTACK;
                    }
                    else
                    {
                        enemyStatus = CharacterStatus.TRACE;
                    }
                }
                else
                {
                    enemyStatus = CharacterStatus.ATTACK;
                }
            }
            else
            {
                if (CollEtcObject())
                {
                    enemyStatus = CharacterStatus.TRACE;
                }
                else
                {
                    enemyStatus = CharacterStatus.MOVE;
                }
            }
            yield return null;
        }
    }
    public override IEnumerator StartAction()
    {
        while (!isDie)
        {
            yield return new WaitUntil(() => Time.timeScale == 1f);
            switch (enemyStatus)
            {
                case CharacterStatus.NONE:
                    break;
                case CharacterStatus.IDLE:
                    animator.SetBool("Move", false);
                    break;
                case CharacterStatus.MOVE:
                    ShowTarget();
                    transform.Translate((Vector3.down * (Speed * 0.1f)) * Time.deltaTime);
                    animator.SetBool("Move", true);
                    break;
                case CharacterStatus.AVODING:
                    if (!recoil)
                    {
                        Avoding();
                        transform.Translate((Vector3.down * (Speed * 0.05f)) * Time.deltaTime);
                        animator.SetBool("Move", true);
                    }
                    break;
                case CharacterStatus.TRACE:
                    animator.SetBool("Move", true);
                    agent.SetDestination(target.transform.position); 
                    break;
                case CharacterStatus.ATTACK:
                    animator.SetBool("Move", false);
                    agent.Stop();
                    ShowTarget();
                    fireCtrl.isFire = true;
                    if (!fireCtrl.isReload && fireCtrl.isFire)
                    {
                        if (Time.time >= fireCtrl.nextFire)        //현재 시간이 다음 발사 시간보다 큰지를 확인
                        {
                            animator.SetTrigger("Attack");
                            StartCoroutine(WeaponRecoil(AvodingTimeMin, AvodingTimeMax));
                            fireCtrl.nextFire = Time.time + fireCtrl.fireRate + Random.Range(0.2f, 0.5f); //다음 발사 시간 계산
                        }
                    }
                    break;
                case CharacterStatus.DIE:
                    isDie = true;
                    animator.SetTrigger("Die");
                    StopAllCoroutines();
                    break;
            }
            yield return null;
        }
    }
}
