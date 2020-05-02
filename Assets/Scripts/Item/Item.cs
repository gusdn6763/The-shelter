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
    public PlayerManager player;
    public e_itemType itemType; // 아이템 종류
    public int itemId; // 아이템 id
    public Sprite itemImage; // 아이템 이미지
    public string itemDes; // 아이템 설명
    public void InitSetting(string _itemName, int _itemId, e_itemType _itemType, string _itemDes, Sprite _itemImage)
    {
        itemName = _itemName;
        itemId = _itemId;
        itemType = _itemType;
        itemDes = _itemDes;
        itemImage = _itemImage;
    }
    //public virtual void Animation(); // 애니메이션 관련 함수 
    public virtual void GetItem()
    {
        Debug.Log("Virtual Activate");
    } // 획득 시 활성화하는 함수
    // 인벤토리가 구현된다면, 인벤토리에 해당 아이템을 넣는 방식
    // 인벤토리가 없다면, 즉시 해당 아이템의 효과를 발휘하는 방식
    void Start()
    {
        player = GameManager.instance.player.GetComponent<PlayerManager>();
    }
}
