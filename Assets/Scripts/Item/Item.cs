using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_itemType
{
    NULL, // 
    WEAPON, // 무기
    MONEY, // 돈
    ARMOR, // 아머
    HEAL, // 체력 회복
    BOOST, // 스텟 강화
}
[System.Serializable]
public class Item : MonoBehaviour
{
    public string itemName; // 이름
    public e_itemType itemType; // 아이템 종류
    public int itemId; // 아이템 id
    public Sprite itemImage; // 아이템 이미지
    public string itemDes; // 아이템 설명
    public void InitSetting(string _itemName, int _itemId, e_itemType _itemType, string _itemDes,
    Sprite _itemImage)
    {
        itemName = _itemName;
        itemId = _itemId;
        itemType = _itemType;
        itemDes = _itemDes;
        itemImage = _itemImage;

        //this.GetComponent<Sprite>();
    }
    //public virtual void Animation(); // 애니메이션 관련 함수 
    //public virtual void GetItem(); // 획득 시 활성화하는 함수
    // 인벤토리가 구현된다면, 인벤토리에 해당 아이템을 넣는 방식
    // 인벤토리가 없다면, 즉시 해당 아이템의 효과를 발휘하는 방식
    void Start()
    {

    }
    void Update()
    {

    }
}

public class Item_Money : Item
{
    public int min_money; // 최소 획득량
    public int max_money; // 최대 획득량
    public void GetItem()
    {
        /*
        ** player.money += Random.Range(min_money, max_money);
        **
        **
        */
        // 플레이어의 돈 정보를 최소에서 최대 획득량만큼 더한다.
    }
}

public class Item_Weapon : Item
{
    public int damage; //
    public void GetItem()
    {
        // 플레이어의 현재 무기와 교체하는 코드
    }
}

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
        **
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

