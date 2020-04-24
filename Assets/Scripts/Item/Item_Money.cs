using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_Money : Item
{
    public int min_money; // 최소 획득량
    public int max_money; // 최대 획득량
    public void GetItem()
    {
        /*
        ** player.money += Random.Range(min_money, max_money);
        ** // Delete This Object.
        **
        */
        // 플레이어의 돈 정보를 최소에서 최대 획득량만큼 더한다.
    }
}
