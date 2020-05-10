using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Mob
{
    private SniperRenderer sniperRenderer;

    private bool waitBeforeShoot = true;

    public override void Start()
    {
        sniperRenderer = GetComponentInChildren<SniperRenderer>();
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
                if (!waitBeforeShoot)
                {
                    enemyStatus = CharacterStatus.ATTACK;
                }
                else
                {
                    enemyStatus = CharacterStatus.IDLE;
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
                    sniperRenderer.ShowLaser();
                    animator.SetBool("Move", false);
                    ShowTarget();
                    if (Time.time >= fireCtrl.nextFire)
                    {
                        waitBeforeShoot = false;
                    }
                        break;
                case CharacterStatus.AVODING:
                    sniperRenderer.DisableLaser();
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
                            sniperRenderer.DisableLaser();
                            waitBeforeShoot = true;
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
}
