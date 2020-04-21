using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public override void InstanceBullet(float bulletCount = 0)       //총알 재생성 함수
    {
        if (bulletCount == 0)
        {
            bulletCount = this.bulletCount;
        }
        for (int i = 0; i < bulletCount; i++)
        {
            BulletCtrl bulletTmp = Instantiate(bullet, firePos.transform);
            bulletTmp.transform.name = "Rifle_Bullet" + i.ToString();
            bulletManager.Add(bulletTmp);
            bulletTmp.gameObject.SetActive(false);
        }
    }
}
