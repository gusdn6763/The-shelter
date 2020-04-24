using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;
public class Gunner : Mob
{
    private PolyNavAgent _agent;
    private PolyNavAgent agent
    {
        get { return _agent != null ? _agent : _agent = GetComponent<PolyNavAgent>(); }
    }

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
