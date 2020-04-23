using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : Mob
{
    public override void Start()
    {
        base.Start();
    }
    public override IEnumerator StartStatus()
    {
        while (!isDie)
        {
            if (enemyStatus == CharacterStatus.DIE) yield break;

            if (FindPlayer())
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
                enemyStatus = CharacterStatus.MOVE;
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
                    //transform.rotation = Quaternion.Euler(0, 0, trans.rotation.z+Time.deltaTime);

                    break;
                case CharacterStatus.ATTACK:
                    animator.SetBool("Move", false);
                    ShowTarget();
                    break;
                case CharacterStatus.DIE:
                    isDie = true;
                    animator.SetTrigger("Die");
                    break;
            }
            yield return null;
        }
    }
    IEnumerator TraceTarget()
    {
        //while()
        yield return new WaitForSeconds(1f);
    }

}
