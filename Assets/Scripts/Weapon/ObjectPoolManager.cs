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
        }

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = Instantiate(weapons[i]).GetComponent<Weapon>();
            weapons[i].InstanceBullet();
            weapons[i].transform.SetParent(transform);
        }
    }




}