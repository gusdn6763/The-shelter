using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CapsuleCollider2D),typeof(FireCtrl))]
public class MovingObject : MonoBehaviour
{
    protected FireCtrl fireCtrl;

    protected CapsuleCollider2D dmgCheck;
    protected Animator animator;

    public GameObject target;

    public float HP;
    public float currentHp;
    public float Armor;
    public float Speed;

    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        dmgCheck = GetComponent<CapsuleCollider2D>();
        fireCtrl = GetComponent<FireCtrl>();
    }

    public virtual void Start()
    {
        currentHp = HP;
    }

    public void ShowTarget()
    {
        Vector3 dir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public virtual void Damaged(float damage)
    {
        if (Armor > 0)
        {
            Armor--;
        }
        else
        {
            currentHp -= damage;
            if (currentHp <= 0)
            {
                Die();
            }
        }
    }

    public virtual void Die()
    {
        dmgCheck.enabled = false;
        Debug.Log(this.gameObject.name+"죽음");
    }
}