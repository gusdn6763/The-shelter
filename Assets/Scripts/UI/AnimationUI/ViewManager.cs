using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ViewManager : MonoBehaviour
{
    protected Animator animator;
    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Open");
    }

    public virtual void Close()
    {
        animator.SetTrigger("Close");
    }

    public virtual void CloseAnimation()
    {
        gameObject.SetActive(false);
    }
}
