using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Mob
{
    public override void Start()
    {
        base.Start();
        startMob();
    }
    public override IEnumerator StartStatus()
    {
        while (!isDie)
        {
            if (enemyStatus == CharacterStatus.DIE) yield break;
            if (FindPlayer())
            {
                if (CollEtcObject())
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
                    enemyStatus = CharacterStatus.FAR_TRACE;
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
            switch (enemyStatus)
            {
                case CharacterStatus.NONE:
                    break;
                case CharacterStatus.IDLE:
                    animator.SetBool("Move", false);
                    break;
                case CharacterStatus.MOVE:
                    ShowTarget();
                    transform.Translate((Vector3.down * (speed * 0.1f)) * Time.deltaTime);
                    animator.SetBool("Move", true);
                    break;
                case CharacterStatus.TRACE:
                    animator.SetBool("Move", true);
                    agent.SetDestination(target.transform.position);

                    break;
                case CharacterStatus.FAR_TRACE:
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
                            fireCtrl.nextFire = Time.time + fireCtrl.fireRate + Random.Range(0.0f, 0.3f); //다음 발사 시간 계산
                        }
                    }
                    break;
                case CharacterStatus.DIE:
                    isDie = true;
                    animator.SetTrigger("Die");
                    break;
            }
            yield return null;
        }
    }
}
