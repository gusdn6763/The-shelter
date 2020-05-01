using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Mob
{
    public bool shotAterAvoding = false;
    public bool recoil = false;
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
                case CharacterStatus.AVODING:
                    if (!recoil)
                    {
                        Avoding();
                        animator.SetBool("Move", true);
                        transform.Translate((Vector3.down * (Speed * 0.05f)) * Time.deltaTime);
                    }
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
                            StartCoroutine(WeaponRecoil());
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

    public override IEnumerator AvodingCoroutine()
    {
        int chooseMovePos = UnityEngine.Random.Range(0, 2);
        if (chooseMovePos == 0)
            AvodingRotate = -AvodingRotate;
        if (this.transform.localPosition.x < 1)
        {
            AvodingRotate = Mathf.Abs(AvodingRotate);
        }
        if (this.transform.localPosition.x > 5)
        {
            if (AvodingRotate > 0)
            {
                AvodingRotate = -AvodingRotate;
            }
        }
        avoding = true;
        transform.rotation = Quaternion.Euler(0, 0, AvodingRotate);
        yield return new WaitUntil(() => fireCtrl.isReload == false);
        avoding = false;
    }
    IEnumerator WeaponRecoil()
    {
        recoil = true;
        yield return new WaitForSeconds(Random.Range(1.2f,1.9f));
        recoil = false;
    }

}
