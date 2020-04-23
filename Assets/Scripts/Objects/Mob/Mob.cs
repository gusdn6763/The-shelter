using System;
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

    [SerializeField] private LayerMask m_viewTargetMask = 1<<10;       // 인식 가능한 타켓의 마스크
    [SerializeField] private LayerMask m_viewObstacleMask = 1<<11;     // 인식 방해물의 마스크 

    private List<Collider2D> hitedTargetContainer = new List<Collider2D>(); // 인식한 물체들을 보관할 컨테이너

    private float m_horizontalViewHalfAngle = 0f; // 시야각의 절반 값


    //Room 스크립트가 몹들의 갯수를 셈
    public Action<Mob> Count;

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
        EVE,
        ATTACK,
        DIE
    }

    public CharacterStatus enemyStatus;
    protected void OnDrawGizmos()
    {
        if (start)
        {
            currentRayPos.origin = transform.position;
            currentRayPos.direction = -transform.up;
            float distance = Vector3.Distance(target.transform.position, transform.position);

            RaycastHit2D rayHitedTarget = Physics2D.Raycast(currentRayPos.origin, currentRayPos.direction, distance, 1 << 10);

            Debug.DrawLine(currentRayPos.origin, rayHitedTarget.point, Color.black);


            m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;

            Vector3 originPos = transform.position;

            Gizmos.DrawWireSphere(originPos, weaponDistance);

            Vector3 horizontalRightDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 horizontalLeftDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 lookDir = AngleToDirZ(m_viewRotateZ);

            Debug.DrawRay(originPos, horizontalLeftDir * weaponDistance, Color.cyan);
            Debug.DrawRay(originPos, lookDir * weaponDistance, Color.green);
            Debug.DrawRay(originPos, horizontalRightDir * weaponDistance, Color.cyan);

            FindViewTargets();
        }
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


    public virtual void Start()
    {
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;
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

    public void startMob()
    {
        start = true;
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

    public void TracePlayer()
    {

    }

    public void OnPlayerDie()
    {
        StopAllCoroutines();
    }

    public bool FindPlayer()
    {
        Vector2 originPos = transform.position;
        Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, weaponDistance, m_viewTargetMask);

        foreach (Collider2D hitedTarget in hitedTargets)
        {
            Vector2 targetPos = hitedTarget.transform.position;
            Vector2 dir = (targetPos - originPos).normalized;
            Vector2 lookDir = AngleToDirZ(m_viewRotateZ);

            float dot = Vector2.Dot(lookDir, dir);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, weaponDistance, m_viewObstacleMask);
            if (rayHitedTarget)
            {
                hitPlayer = rayHitedTarget;
                Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow);
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
}
