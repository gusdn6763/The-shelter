using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CapsuleCollider2D),typeof(Weapon))]
public class MovingObject : MonoBehaviour
{
    protected Weapon weapon;                   //총 발사를 제어하는 Weapon 클래스를 저장할 변수

    protected Animator animator;

    protected CapsuleCollider2D dmgCheck;

    public GameObject target;

    protected bool playerOrEnemyBulletCheck;    //총알이 플레이어것인지 몹인지 체크
    public float FullHp;
    public float currentHp;
    public float speed;
    public float add_Damage;                        //총알의 추가 데미지

    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        dmgCheck = GetComponent<CapsuleCollider2D>();
        weapon = GetComponent<Weapon>();

        FullHp = 100;
        speed = 10f;
        add_Damage = 10f;
    }

    public void ShowTarget()
    {
        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Damaged(float damage)
    {
        currentHp -= damage;
        if (currentHp<=0)
        {
            Die();
        } 
    }

    public virtual void Die()
    {
        dmgCheck.enabled = false;
        Debug.Log(this.gameObject.name+"죽음");
    }
}