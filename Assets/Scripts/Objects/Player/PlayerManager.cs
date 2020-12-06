using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{
    public static PlayerManager instance;

    public List<Mob> mobs = new List<Mob>();

    public int currentMoney;
    public enum CharacterStatus
    {
        NONE,
        IDLE,
        MOVE,
        ATTACK,
        DIE
    }

    public CharacterStatus playerStatus;

    public override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void Update()
    {
        if (playerStatus != CharacterStatus.NONE)
        {
            switch (playerStatus)
            {
                case CharacterStatus.IDLE:
                    if (mobs.Count != 0)
                    {
                        playerStatus = CharacterStatus.ATTACK;
                    }
                    animator.SetBool("Move", false);
                    break;
                case CharacterStatus.MOVE:
                    transform.Translate((Vector3.down * (Speed * 0.1f)) * Time.deltaTime);
                    animator.SetBool("Move", true);
                    break;
                case CharacterStatus.ATTACK:
                    FindTarget();
                    fireCtrl.isFire = true;
                    if (!fireCtrl.isReload && fireCtrl.isFire)
                    {
                        if (Time.time >= fireCtrl.nextFire)        //현재 시간이 다음 발사 시간보다 큰지를 확인
                        {
                            animator.SetTrigger("Attack_Rifle");
                            fireCtrl.nextFire = Time.time + fireCtrl.fireRate + Random.Range(0.0f, 0.3f); //다음 발사 시간 계산
                        }
                    }
                    break;
                case CharacterStatus.DIE:
                    animator.SetTrigger("Die");
                    playerStatus = CharacterStatus.NONE;
                    break;
            }
        }
    }

    private void OnEnable()
    {
        fireCtrl.isReload = false;
    }

    public void Move(float distance,float startTime)        
    {
        StartCoroutine(MoveCoroutine(distance, startTime));
    }

    public void ReserPlayer()
    {
        HP = 100f;
        currentHp = 100f;
        Armor = 0;
        GetComponent<FireCtrl>().remainingBullet = 10;
        //player.transform.position = new Vector3(0, -4f, 0);
    }

    public void FindTarget()
    {
        float distanceTmp = 64;

        for (int i = 0; i < mobs.Count; i++)
        {

            float Distance = Vector3.Distance(transform.position, mobs[i].transform.position);

            if (Distance < distanceTmp)
            {
                target = mobs[i].gameObject;

                distanceTmp = Distance;
                ShowTarget();
            }
        }
    }

    public void ResetInfo()
    {
        HP = 100f;
        currentHp = 100f;
        Armor = 0;
        GetComponent<FireCtrl>().remainingBullet = 10;
    }

    IEnumerator MoveCoroutine(float distance,float startTime)
    {
        Vector3 toPos = new Vector3(transform.position.x, transform.position.y+distance, transform.position.z);
        while (startTime > 0)
        {
            startTime -= Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, toPos,(startTime+(Time.deltaTime/startTime))-startTime);
            yield return null;
        }
        playerStatus = CharacterStatus.IDLE;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Item"))
        {
            col.gameObject.GetComponent<Item>().GetItem(this);
        }
    }

    public override void Die()
    {
        base.Die();
        playerStatus = CharacterStatus.DIE;
    }

    public void Dead()
    {
        GameManager.instance.FailedStage();
        StopAllCoroutines();
        dmgCheck.enabled = true;
        gameObject.SetActive(false);
    }
}
