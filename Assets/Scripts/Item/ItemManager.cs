using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private GameObject itemDatabase;
    private ItemDatabase itemScript;
    private Transform itemParent;
    public GameObject droppedEffect;
    void GenerateItem(Vector3 position, int id) // 1개 생성
    {
        GameObject tmp;
        GameObject item = itemScript.itemList.Find(x => x.GetComponent<Item>().itemId == id);
        if (item != null)
        {
            tmp = Instantiate(item);
            tmp.transform.position = position;
            tmp.transform.SetParent(itemParent);
        }
    }
    void GenerateItem(Vector3 position, int id, int count) // 여러개 생성. 사용하지는 않을 듯
    {
        GameObject tmp;
        GameObject item = itemScript.itemList.Find(x => x.GetComponent<Item>().itemId == id);
        while (item != null && count > 0)
        {
            tmp = Instantiate(item);
            tmp.transform.position = position;
            tmp.transform.SetParent(itemParent);
            count--;
        }
    }
    // 특정 범위 내 랜덤 생성
    void GenerateItem(Vector3 position, float radius, int id, int count)
    {
        float temp_radius = Mathf.Sqrt(Random.Range(0f, radius * radius));
        float temp_angle = Random.Range(0f, 2f) * Mathf.PI;
        Vector3 temp_position = new Vector3(position.x + temp_radius * Mathf.Cos(temp_angle),
        position.y + temp_radius * Mathf.Sin(temp_angle), position.y);
        GameObject tmp;
        GameObject item = itemScript.itemList.Find(x => x.GetComponent<Item>().itemId == id);
        while (item != null && count > 0)
        {
            tmp = Instantiate(item);
            tmp.transform.position = temp_position;
            temp_radius = Mathf.Sqrt(Random.Range(0f, radius * radius));
            temp_angle = Random.Range(0f, 2f) * Mathf.PI;
            temp_position = new Vector3(position.x + temp_radius * Mathf.Cos(temp_angle),
        position.y + temp_radius * Mathf.Sin(temp_angle), position.y);
            tmp.transform.SetParent(itemParent);
            count--;
            //Debug.Log(temp_radius);
        }
    }

    void GenerateItem(Vector3 position, float radius, int id, int count, float speed)
    {

        float temp_radius = Mathf.Sqrt(Random.Range(0f, radius * radius));
        float temp_angle = Random.Range(0f, 2f) * Mathf.PI;
        Vector2 temp_position = new Vector2(position.x + temp_radius * Mathf.Cos(temp_angle),
        position.y + temp_radius * Mathf.Sin(temp_angle));
        GameObject tmp;
        GameObject item = itemScript.itemList.Find(x => x.GetComponent<Item>().itemId == id);
        while (item != null && count > 0)
        {
            tmp = Instantiate(droppedEffect);
            tmp.transform.position = position;
            StartCoroutine(MoveItem(tmp, temp_position, id, speed));
            temp_radius = Mathf.Sqrt(Random.Range(0f, radius * radius));
            temp_angle = Random.Range(0f, 2f) * Mathf.PI;
            temp_position = new Vector2(position.x + temp_radius * Mathf.Cos(temp_angle),
        position.y + temp_radius * Mathf.Sin(temp_angle));
            tmp.transform.SetParent(itemParent);
            count--;
            //Debug.Log(temp_radius);
        }
    }

    IEnumerator MoveItem(GameObject tmp, Vector2 dest, int id, float speed)
    {
        float count = 0;
        Vector2 wasPos = tmp.transform.position;
        while (true)
        {
            count += Time.deltaTime;
            tmp.transform.position = Vector2.Lerp(wasPos, dest, speed * count);
            if (count >= 1)
            {
                tmp.transform.position = dest;
                GenerateItem(new Vector3(dest.x, dest.y, 0f), id);
                Destroy(tmp);
                break ;
            }
            yield return null;
        }
    }
    /*
    **
    **
    */
    void Awake()
    {
        itemParent = transform.GetChild(0);

        itemDatabase = GameObject.Find("ItemDatabase");
        itemScript = itemDatabase.GetComponent<ItemDatabase>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountTime(2f)); //for debugging
        Debug.Log("Next line for Coroutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CountTime(float delayTime)
    {
        GenerateItem(new Vector3(0f, 1f, 0), 1f, 1, 2, 3);
        GenerateItem(new Vector3(0f, 1f, 0), 1f, 2, 2, 3);
        GenerateItem(new Vector3(0f, 1f, 0), 1f, 3, 2, 3);
        //Debug.Log("Generate Item");
        yield return new WaitForSeconds(delayTime);
        //Debug.Log("Are you watiing?");
        StartCoroutine(CountTime(2f));
    }
}
