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
    public string sound;

    public virtual void GetItem(PlayerManager player)
    {
        SoundManager.instance.PlaySE(sound);
        Debug.Log("Virtual Activate");
    }
}
