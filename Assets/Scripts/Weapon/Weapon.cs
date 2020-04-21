using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<BulletCtrl> bulletManager = new List<BulletCtrl>();

    public BulletCtrl bullet;

    public Transform firePos;

    public float bulletCount;
    public float nextFire = 0.0f;               //다음 발사할 시간 계산용 변수
    public readonly float reloadTime = 2.0f;    //재장전 시간
    public readonly int maxBullet = 10;         //탄창의 최대 총알 수
    public int currBullet = 10;                 //초기 총알 수
    public bool isReload = false;               //재장전 여부
    public WaitForSeconds wsReload;             //재장전 시간 동안 기다릴 변수 선언
    public bool isFire = false;                  //총 발사 여부를 판단할 변수                                            
    public readonly float fireRate = 0.1f;      //총알 발사 간격

    private void Awake()
    {
        InstanceBullet(10);
    }
    public virtual void InstanceBullet(float bulletCount = 0)       //총알 재생성 함수
    {
        if (bulletCount == 0)
        {
            bulletCount = this.bulletCount;
        }
        for(int i=0;i< bulletCount; i++)
        {
            BulletCtrl bulletTmp = Instantiate(bullet,firePos.transform);
            bulletTmp.transform.name = "Bullet"+i.ToString();
            bulletManager.Add(bulletTmp);
            bulletTmp.gameObject.SetActive(false);
        }
    }
    IEnumerator Reloading()                                         //총알 재장전 함수
    {
        //재장전 애니메이션 실행
        //animator.SetTrigger(hashReload);
        //재장전 사운드 발생
        //audio.PlayOneShot(reloadSfx, 1.0f);
        //재장전 시간만큼 대기하는 동안 제어권 양보
        yield return wsReload;
        //총알의 개수를 초기화
        currBullet = maxBullet;
        isReload = false;
    }

    public void Fire(bool check,Transform pos,float damage)
    {
        int Count = bulletManager.Count;
        for (int i = 0; i < Count; i++)
        {
            if (bulletManager[i].gameObject.activeSelf)
            {
                if (i == Count - 1)
                {
                    InstanceBullet(1);
                    BulletActive(check, damage,pos, bulletManager[i+1]);
                    break;
                }
                continue;
            }
            BulletActive(check, damage, pos, bulletManager[i]);
            break;
        }
    }

    public void BulletActive(bool check, float damage,Transform pos, BulletCtrl bullet)
    {
        bullet.playerBullet = check;
        bullet.transform.position = pos.position;
        bullet.transform.rotation = pos.rotation;
        bullet.gameObject.SetActive(true);  
        return;
    }
}
