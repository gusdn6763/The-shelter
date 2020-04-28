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

            if (fireCtrl.isReload)
            {
                enemyStatus = CharacterStatus.AVODING;
            }
            else
            {
                enemyStatus = CharacterStatus.ATTACK;
            }
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
                    break;
                case CharacterStatus.MOVE:
                    break;
                case CharacterStatus.AVODING:
                    Avoding();
                    transform.Translate((Vector3.down * (speed * 0.05f)) * Time.deltaTime);
                    animator.SetBool("Move", true);
                    break;
                case CharacterStatus.TRACE:
                    break;
                case CharacterStatus.FAR_TRACE:
                    break;
                case CharacterStatus.ATTACK:
                    animator.SetBool("Move", false);
                    agent.Stop();
                    ShowTarget();
                    fireCtrl.isFire = true;
                    if (!fireCtrl.isReload && fireCtrl.isFire)
                    {
                        if (Time.time >= fireCtrl.nextFire)
                        {
                            animator.SetTrigger("Attack");
                            fireCtrl.nextFire = Time.time + fireCtrl.fireRate + Random.Range(0.0f, 0.3f);
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
