using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_Consumption : Item
{
    public float instant_heal; // 즉시 회복 시
    public float duration_time; // 버프 식으로 회복 시 지속 시간
    private float duration_temp_time; 
    public float duration_heal; // 지속적 회복량(틱 or sec 당)
    private float tick = 1; // 지속 회복 시 기준
    public override void GetItem(PlayerManager player)
    {
        Debug.Log("GetItem Activate");
        duration_temp_time = duration_time;
        player.currentHp += instant_heal;
        if (player.currentHp > player.HP)
            player.currentHp = player.HP;
        if (duration_heal == 0)
            Destroy(gameObject);
    }
}