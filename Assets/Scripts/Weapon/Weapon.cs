﻿using System.Collections;
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


    public void Fire(bool check,Transform pos,float damage,float distance, float speed, bool passObstacles)
    {
        int Count = bulletManager.Count;
        for (int i = 0; i < Count; i++)
        {
            if (bulletManager[i].gameObject.activeSelf)
            {
                if (i == Count - 1)
                {
                    InstanceBullet(1);
                    BulletActive(check, damage,distance,speed,pos, bulletManager[i+1], passObstacles);
                    break;
                }
                continue;
            }
            BulletActive(check, damage, distance, speed, pos, bulletManager[i], passObstacles);
            break;
        }
    }

    public void BulletActive(bool check, float damage,float disance,float speed,Transform pos, BulletCtrl bullet,bool passObstacles)
    {
        bullet.playerBullet = check;
        bullet.transform.position = pos.position;
        bullet.transform.rotation = pos.rotation;
        bullet.damage = damage;
        bullet.distance = disance;
        bullet.speed = speed;
        bullet.passObstacle = passObstacles;
        bullet.gameObject.SetActive(true);
        return;
    }
}
