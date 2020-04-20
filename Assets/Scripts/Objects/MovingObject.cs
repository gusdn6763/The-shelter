using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CapsuleCollider2D))]
public class MovingObject : MonoBehaviour
{
    protected Animator animator;

    protected CapsuleCollider2D dmgCheck;

    public GameObject target;

    public float FullHp;
    public float currentHp;
    public float speed;
    public float attack_Delay;
    public float attack_Delay_Tmp;
    public float add_Damage;

    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        dmgCheck = GetComponent<CapsuleCollider2D>();

        FullHp = 100;
        speed = 10f;
        attack_Delay = 1f;
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