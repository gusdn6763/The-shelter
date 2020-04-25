﻿using System.Collections;
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
    public override void GetItem()
    {
        Debug.Log("GetItem Activate");
        duration_temp_time = duration_time;
        player.currentHp += instant_heal;
        if (player.currentHp > player.FullHp)
            player.currentHp = player.FullHp;
        if (duration_heal == 0)
            Destroy(gameObject);
        StartCoroutine(Continuing_Heal(tick));
    }
    IEnumerator Continuing_Heal(float delayTime)
    {
        player.currentHp += duration_heal;
        if (player.currentHp > player.FullHp)
            player.currentHp = player.FullHp;
        duration_temp_time -= delayTime;
        if (duration_temp_time <= 0)
            Destroy(gameObject);
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(Continuing_Heal(delayTime));
    }
}