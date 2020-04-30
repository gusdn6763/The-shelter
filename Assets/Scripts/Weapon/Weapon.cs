using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<BulletCtrl> bulletManager = new List<BulletCtrl>();

    public BulletCtrl bullet;

    public Transform firePos;

    public float bulletCount;
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
        bullet.damage += damage;
        bullet.gameObject.SetActive(true);
        return;
    }
}
