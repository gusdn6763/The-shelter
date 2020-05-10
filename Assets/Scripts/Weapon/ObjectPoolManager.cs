using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    public Weapon[] weapons;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = transform.GetChild(i).GetComponent<Weapon>();
            weapons[i].InstanceBullet(10);
        }
    }
}