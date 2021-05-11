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
    protected EnemyUI enemyUI;

    [Header("View Config")]
    [Range(0f, 360f)]
    [SerializeField] private float obstViewAngle = 0f; // 시야 범위 값, 장애물 회피 여부
    private float m_horizontalViewHalfAngle = 0f; // 시야각의 절반 값

    public LayerMask m_viewTargetMask;       // 인식 가능한 타켓의 마스크
    public LayerMask m_viewObstacleMask;     // 인식 방해물의 마스크 

    protected Ray2D currentRayPos = new Ray2D();        //현재 적이 보고있는 방향
    protected Ray2D playerRay = new Ray2D();            //플레이어의 방향
    protected RaycastHit2D hitPlayer;                   //몹의 방향에 플레이어가 맞았는지 확인하는 여부
    protected RaycastHit2D hitEtcObject;

    private float m_viewRotateZ = -180f; // 보고있는 z의 값
    public float weaponDistance;

    [Header("Avoding Value")]
    [Range(0f, 360f)]
    public float AvodingRotate;
    [Range(0f, 10f)]
    public float AvodingTimeMin;
    [Range(0f, 10f)]
    public float AvodingTimeMax;
    protected bool avoding = false;
    protected bool recoil = false;

    public bool isDie = false;
    public bool start = false;
    [Header("ItemDrop")]
    [Range(0f, 100f)]
    public float itemDropPercent;

    Vector3 lookDir;
    public enum CharacterStatus
    {
        NONE,
        IDLE,
        MOVE,
        TRACE,
        AVODING,
        ATTACK,
        DIE
    }

    public CharacterStatus enemyStatus;

    public override void Start()
    {
        enemyUI = GetComponent<EnemyUI>();
        base.Start();
        target = GameManager.instance.player.gameObject;
        m_horizontalViewHalfAngle = obstViewAngle * 0.5f;
        startMob();

    }
    protected void OnDrawGizmos()
    {
        if (start)
        {
            target = GameManager.instance.player.gameObject;
            currentRayPos.origin = transform.position;
            currentRayPos.direction = -transform.up;
            playerRay.origin = target.transform.position;
            
            Debug.DrawLine(currentRayPos.origin, playerRay.origin, Color.black);

            m_horizontalViewHalfAngle = obstViewAngle * 0.5f;

            Gizmos.DrawWireSphere(currentRayPos.origin, weaponDistance);

            Vector3 horizontalLeftDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 horizontalRightDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);
            lookDir = AngleToDirZ(m_viewRotateZ);

            Debug.DrawRay(currentRayPos.origin, horizontalLeftDir * weaponDistance, Color.cyan);
            Debug.DrawRay(currentRayPos.origin, lookDir * weaponDistance, Color.green);
            Debug.DrawRay(currentRayPos.origin, horizontalRightDir * weaponDistance, Color.cyan);

            Vector2 originPos = transform.position;
            Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, weaponDistance, m_viewTargetMask);
            //Debug.DrawRay(transform.position, -transform.up * 2f * 110, Color.white);
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
    public void Avoding()
    {
        if (avoding == false)
        {
            StartCoroutine(AvodingCoroutine());
        }
    }
    public virtual IEnumerator AvodingCoroutine()
    {
        int chooseMovePos = UnityEngine.Random.Range(0, 2);
        if (chooseMovePos == 0)
            AvodingRotate = -AvodingRotate;
        if (transform.localPosition.x < 1)
            AvodingRotate = Mathf.Abs(AvodingRotate);
        if (transform.localPosition.x > 5)
        {
            if (AvodingRotate > 0)
                AvodingRotate = -AvodingRotate;
        }
        transform.Rotate(0, 0, UnityEngine.Random.Range(AvodingRotate, AvodingRotate));

        ////왜 안되니이이이이이
        //RaycastHit2D rayHitedTarget = Physics2D.Raycast(transform.position, -transform.up * 2f, weaponDistance, m_viewTargetMask);

        //if (rayHitedTarget)
        //{
        //    print(rayHitedTarget.transform.gameObject.name);
        //    while (!rayHitedTarget)
        //    {
        //        lookDir = AngleToDirZ(m_viewRotateZ);
        //        rayHitedTarget = Physics2D.Raycast(transform.position, lookDir, weaponDistance, m_viewTargetMask);
        //        transform.Rotate(0, 0, UnityEngine.Random.Range(AvodingRotate, AvodingRotate));
        //        if (chooseMovePos == 0)
        //            AvodingRotate--;
        //        else
        //            AvodingRotate++;
        //        yield return null;
        //    }
        //}
        avoding = true;
        transform.Rotate(0, 0, UnityEngine.Random.Range(AvodingRotate - 10, AvodingRotate + 10));
        yield return new WaitUntil(() => fireCtrl.isReload == false);
        avoding = false;
    }
    public IEnumerator WeaponRecoil(float a, float b)
    {
        recoil = true;
        yield return new WaitForSeconds(UnityEngine.Random.Range(a, b));
        recoil = false;
    }
    public override void Damaged(float damage)
    {
        base.Damaged(damage);
        enemyUI.DamagedBar(currentHp / HP);
    }
    public override void Die()
    {
        GameManager.instance.player.mobs.Remove(this);
        enemyStatus = CharacterStatus.DIE;
        agent.Stop();
        float percent = UnityEngine.Random.Range(0, 101);
        if (itemDropPercent >= percent)
        {
            ItemManager.instance.GenerateItem(this.gameObject.transform.position, 0.6f, UnityEngine.Random.Range(1, 4), 1, 1f);
        }
        base.Die();
    }
    public void Dead()              //애니메이션에서 실행
    {
        enemyUI.DestoryUI();
        Destroy(this.gameObject);
    }

}
