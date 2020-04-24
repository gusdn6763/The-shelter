using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_Consumption : Item
{
    public int instant_heal; // 즉시 회복 시
    public float duration_time; // 버프 식으로 회복 시 지속 시간
    private float duration_temp_time; 
    public int duration_heal; // 지속적 회복량(틱 or sec 당)
    private float tick; // 지속 회복 시 기준
    public void GetItem()
    {
        /*
        ** duration_temp_time = duration_time;
        ** player.hp += instant_heal;
        ** StartCoroutine(Continuing_Heal(tick));
        ** //Delete this Item Object
        */
    }
    /*
    IEnumerator Continuing_Heal(float delayTime)
    {
        player.hp += duration_heal;
        duration_temp_time -= delayTime;
        if (duration_temp_time > 0)
        {
            yield return new WaitForSeconds(delayTime);
            StartCoroutine(Continuing_Heal(delayTime));
        }
    }
    */
}