using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ViewManager : MonoBehaviour
{
    Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // 팝업 나타날때 호출할 함수
    public void Open()
    {
        animator.SetTrigger("Open");
    }

    public void Close()
    {
        animator.SetTrigger("Close");
    }
}
