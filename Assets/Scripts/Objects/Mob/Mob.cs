using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;
public class Mob : MovingObject
{
    protected PolyNavAgent _agent;
    public PolyNavAgent agent
    {
        get { return _agent != null ? _agent : _agent = GetComponent<PolyNavAgent>(); }
    }

    [Header("View Config")]
    [Range(0f, 360f)]
    [SerializeField] private float m_horizontalViewAngle = 0f; // 시야 범위 값
    private float m_horizontalViewHalfAngle = 0f; // 시야각의 절반 값

    public LayerMask m_viewTargetMask;       // 인식 가능한 타켓의 마스크
    public LayerMask m_viewObstacleMask;     // 인식 방해물의 마스크 

    public Action<Mob> Count;       //방에서 죽으면 몹의 갯수를 빼줄 딜리게이트

    protected Ray2D currentRayPos = new Ray2D();        //현재 적이 보고있는 방향
    protected Ray2D playerRay = new Ray2D();            //플레이어의 방향
    protected RaycastHit2D hitPlayer;                   //몹의 방향에 플레이어가 맞았는지 확인하는 여부
    protected RaycastHit2D hitEtcObject;

    private float m_viewRotateZ = -180f; // 보고있는 z의 값
    private bool avoding = false;

    public float weaponDistance;
    public float AvodingRotate;
    public bool isDie = false;
    public bool start = false;
    

    public enum CharacterStatus
    {
        NONE,
        IDLE,
        MOVE,
        TRACE,
        FAR_TRACE,
        AVODING,
        ATTACK,
        DIE
    }

    public CharacterStatus enemyStatus;

    public override void Start()
    {
        base.Start();
        target = GameManager.instance.player.gameObject;
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;
        startMob();
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

            Vector2 originPos = transform.position;
            Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, weaponDistance, m_viewTargetMask);

            foreach (Collider2D hitedTarget in hitedTargets)
            {
                Vector2 targetPos = hitedTarget.transform.position;
                Vector2 dir = (targetPos - originPos).normalized;

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
                        Debug.DrawLine(originPos, targetPos, Color.red);
                    }
                }
            }
        }
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
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
    public virtual void Avoding(float rotate = 0f)
    {
        if (avoding == false)
        {
            StartCoroutine(AvodingCoroutine(rotate));
        }
    }
    public virtual IEnumerator AvodingCoroutine(float rotate = 0f)
    {
        int chooseMovePos = UnityEngine.Random.Range(0, 1);
        if (chooseMovePos == 0)
            rotate = -rotate;
        avoding = true;
        transform.Rotate(0, 0, UnityEngine.Random.Range(rotate - 10, rotate + 10));
        yield return new WaitUntil(() => fireCtrl.isReload == false);
        avoding = false;
    }
    public override void Damaged(float damage)
    {
        base.Damaged(damage);
        GetComponent<EnemyUI>().DamagedBar(currentHp / HP);
    }
    public override void Die()
    {
        base.Die();
        enemyStatus = CharacterStatus.DIE;
    }
    public void Dead()              //애니메이션에서 실행
    {
        //Count(this);
        Destroy(this.gameObject);
    }

}
