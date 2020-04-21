using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class BulletCtrl : MonoBehaviour 
{
    private Animator animator;
    private Transform trans;

    public bool playerBullet;
    public float speed;
    public float distance;
    private float distanceTmp;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        trans = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        StartCoroutine(BulletMove());
    }

    private void OnDisable()
    {
        trans.position = Vector3.zero;
        trans.rotation = Quaternion.identity;
    }

    IEnumerator BulletMove()
    {
        distanceTmp = distance;
        while (distanceTmp >= 0)
        {
            distanceTmp -= Time.deltaTime * speed;
            transform.Translate(-transform.up * speed * Time.deltaTime, Space.World);
            yield return null;
        }
        Debug.Log("사라짐");
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (playerBullet)
        {
            if (Col.gameObject.CompareTag("Enemy"))
            {
                //Col.GetComponent<MovingObject>().Damaged(damage);
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (Col.gameObject.CompareTag("Player"))
            {
                //Col.GetComponent<MovingObject>().Damaged(damage);
                gameObject.SetActive(false);
            }
        }
    }

}
