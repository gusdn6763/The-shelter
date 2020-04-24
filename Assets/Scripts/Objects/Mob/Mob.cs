﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MovingObject
{
    [Header("View Config")]
    [Range(0f, 360f)]
    [SerializeField] private float m_horizontalViewAngle = 0f; // 시야각
    [Range(-180f, 180f)]
    [SerializeField] private float m_viewRotateZ = 0f; // 시야각의 회전값

    public LayerMask m_viewTargetMask;       // 인식 가능한 타켓의 마스크
    public LayerMask m_viewObstacleMask;     // 인식 방해물의 마스크 

    private List<Collider2D> hitedTargetContainer = new List<Collider2D>(); // 인식한 물체들을 보관할 컨테이너

    private float m_horizontalViewHalfAngle = 0f; // 시야각의 절반 값

    public Action<Mob> Count;       //방에서 죽으면 몹의 갯수를 빼줄 딜리게이트

    protected RaycastHit2D raycastHit;
    protected Ray2D currentRayPos = new Ray2D();        //현재 적이 보고있는 방향
    protected Ray2D playerRay = new Ray2D();            //플레이어의 방향
    protected RaycastHit2D hitPlayer;
    protected RaycastHit2D hitEtcObject;
    public float weaponDistance;

    public bool start = false;
    public bool isDie = false;

    public enum CharacterStatus
    {
        NONE,
        IDLE,
        MOVE,
        TRACE,
        FAR_TRACE,
        EVE,
        ATTACK,
        DIE
    }

    public CharacterStatus enemyStatus;

    public override void Awake()
    {
        base.Awake();
        
    }
    public virtual void Start()
    {
        target = GameManager.instance.player.gameObject;
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;
    }
    protected void OnDrawGizmos()
    {
        if (start)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            currentRayPos.origin = transform.position;
            currentRayPos.direction = -transform.up;
            playerRay.origin = target.transform.position;
            playerRay.direction = target.transform.position;
            
            Debug.DrawLine(currentRayPos.origin, playerRay.origin, Color.black);

            m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;

            Gizmos.DrawWireSphere(currentRayPos.origin, weaponDistance);

            Vector3 horizontalLeftDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 horizontalRightDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 lookDir = AngleToDirZ(m_viewRotateZ);

            Debug.DrawRay(currentRayPos.origin, horizontalLeftDir * weaponDistance, Color.cyan);
            Debug.DrawRay(currentRayPos.origin, lookDir * weaponDistance, Color.green);
            Debug.DrawRay(currentRayPos.origin, horizontalRightDir * weaponDistance, Color.cyan);

            FindViewTargets();
        }
    }
    public void TracePlayer()
    {

    }

    public Collider2D[] FindViewTargets()
    {
        hitedTargetContainer.Clear();

        Vector2 originPos = transform.position;
        Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, weaponDistance, m_viewTargetMask);

        foreach (Collider2D hitedTarget in hitedTargets)
        {
            Vector2 targetPos = hitedTarget.transform.position;
            Vector2 dir = (targetPos - originPos).normalized;
            Vector2 lookDir = AngleToDirZ(m_viewRotateZ);

            float dot = Vector2.Dot(lookDir, dir);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (angle <= m_horizontalViewHalfAngle)
            {
                RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, weaponDistance, m_viewObstacleMask);
                if (rayHitedTarget)
                {
                    Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow);
                }
                else
                {
                    hitedTargetContainer.Add(hitedTarget);
                    Debug.DrawLine(originPos, targetPos, Color.red);
                }
            }
        }

        if (hitedTargetContainer.Count > 0)
            return hitedTargetContainer.ToArray();
        else
            return null;
    }
    private Vector2 AngleToDirZ(float angleInDegree)
    {
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian));
    }

    public void startMob()
    {
        start = true;
        ShowTarget();
        StartCoroutine(StartStatus());
        StartCoroutine(StartAction());
    }

    public virtual IEnumerator StartStatus()
    {
        yield return null;
    }

    public virtual IEnumerator StartAction()
    {
        yield return null;
    }

    public bool FindPlayer()
    {
        Vector2 originPos = transform.position;
        Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, weaponDistance);
        foreach (Collider2D hitedTarget in hitedTargets)
        {
            Vector2 targetPos = hitedTarget.transform.position;
            Vector2 dir = (targetPos - originPos).normalized;
            Vector2 lookDir = AngleToDirZ(m_viewRotateZ);

            float dot = Vector2.Dot(lookDir, dir);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, weaponDistance, m_viewTargetMask);
            if (rayHitedTarget)
            {
                hitPlayer = rayHitedTarget;
                Debug.DrawLine(originPos, rayHitedTarget.point, Color.red);
                return true;
            }
        }
        return false;
    }
    public bool CollEtcObject()
    {
        RaycastHit2D rayHitedTarget = Physics2D.Raycast(transform.position, -transform.up, weaponDistance, m_viewObstacleMask);
        if (rayHitedTarget)
        {
            hitEtcObject = rayHitedTarget;
            return true;
        }
        return false;
    }
    public bool Comparison()
    {
        if (hitEtcObject.distance > hitPlayer.distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Dead()              //애니메이션에서 실행
    {
        Count(this);    
        Destroy(this.gameObject);
    }

    public override void Die()
    {
        base.Die();
        enemyStatus = CharacterStatus.DIE;
    }



    public void OnPlayerDie()
    {
        StopAllCoroutines();
    }

}
