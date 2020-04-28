using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CapsuleCollider2D),typeof(FireCtrl))]
public class MovingObject : MonoBehaviour
{
    protected Animator animator;

    protected CapsuleCollider2D dmgCheck;

    protected FireCtrl fireCtrl;

    public GameObject target;

    public float FullHp;
    public float currentHp;
    public float speed;
    public float currentArmor;
    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        dmgCheck = GetComponent<CapsuleCollider2D>();
        fireCtrl = GetComponent<FireCtrl>();

        FullHp = 100;
        speed = 10f;
    }

    public void ShowTarget()
    {
        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Damaged(float damage)
    {
        if (currentArmor > 0)
        {
            currentArmor -= damage;
            if (currentArmor < 0)
                damage = -currentArmor;
            else return ;
        }
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