using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private GameObject itemDatabase;
    private ItemDatabase itemScript;
    private Transform itemParent;
    void GenerateItem(Vector3 position, int id) // 1개 생성
    {
        GameObject tmp;
        GameObject item = itemScript.itemList.Find(x => x.GetComponent<Item>().itemId == id);
        if (item)
        {
            tmp = Instantiate(item);
            tmp.transform.position = position;
            tmp.transform.SetParent(itemParent);
        }
    }
    void GenerateItem(Vector3 position, int id, int count) // 여러개 생성
    {

    }
    // 특정 범위 내 랜덤 생성
    void GenerateItem(Vector3 position, float radisu, int id, int count)
    {
        
    }
    void Awake()
    {
        itemParent = transform.GetChild(0);

        itemDatabase = GameObject.Find("ItemDatabase");
        itemScript = itemDatabase.GetComponent<ItemDatabase>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CountTime(0.3f)); for debugging
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /* for debugging
    IEnumerator CountTime(float delayTime)
    {
        GenerateItem(new Vector3(3f, 3f, 0), 1);
        Debug.Log("Generate Item");
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(CountTime(0.3f));
    }
    */
}
