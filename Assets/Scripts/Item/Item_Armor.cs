using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_Armor : Item
{
    public int armor_amout; // 최대 막아줄 수 있는 피해
    public override void GetItem(PlayerManager player)
    {
        player.Armor += armor_amout;
        Destroy(gameObject);
    }
}