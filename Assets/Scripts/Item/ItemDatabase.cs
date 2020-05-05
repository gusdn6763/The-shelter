using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    public List<GameObject> itemList;

    void Awake()
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
    }

    void Start()
    {
        //Add("money_0", 0, "Nothing to explicate this.", e_itemType.MONEY);
    }

    void Add(string itemName, int itemId, string itemDes, e_itemType itemType)
    {
        //tmp.InitSetting(itemName, itemId, itemType, itemDes, Resources.Load<Sprite>("ItemImage/" + itemName));
        //itemList.Add(tmp);
    }
}
