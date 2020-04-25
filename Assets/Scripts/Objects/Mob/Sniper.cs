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
            enemyStatus = CharacterStatus.ATTACK;
            enemyStatus = CharacterStatus.IDLE;
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
                    break;
                case CharacterStatus.MOVE:
                    break;
                case CharacterStatus.TRACE:
                    break;
                case CharacterStatus.FAR_TRACE:
                    break;
                case CharacterStatus.ATTACK:
                    break;
                case CharacterStatus.DIE:
                    break;
            }
            yield return null;
        }
    }
}
