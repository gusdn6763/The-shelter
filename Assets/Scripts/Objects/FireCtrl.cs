using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public Transform trans;
    protected Weapon weapon;                   //총 발사를 제어하는 Weapon 클래스를 저장할 변수

    public bool playerOrEnemyBulletCheck;       //총알이 플레이어것인지 몹인지 체크
    public int maxBullet = 10;                  //탄창의 최대 총알 수
    public int remainingBullet = 10;            //남은 총알 수
    public float nextFire = 0.0f;               //다음 발사할 시간 계산용 변수
    public float reloadTime = 2.0f;             //재장전 시간
    public bool isReload = false;               //재장전 여부
    public bool isFire = false;                 //총 발사 여부를 판단할 변수 
    [Header("총알 연사 속도")]
    public float fireRate = 0.3f;               //총알 발사 간격
    public float add_Damage;                    //총알의 추가 데미지                                              

    private void Awake()
    {
        add_Damage = 10f;
    }

    private void Start()
    {
        weapon = ObjectPoolManager.instance.weapons[0];
    }

    public void Fire_Check()
    {
        if (!isReload)
        {
            --remainingBullet;      //총알 수를 하나 감소
            weapon.Fire(playerOrEnemyBulletCheck, trans, add_Damage);
            //남은 총알이 없을 경우 재장전 코루틴 호출
            if (remainingBullet == 0)
            {
                StartCoroutine(Reloading());
            }
        }
    }

    IEnumerator Reloading()                                         //총알 재장전 함수
    {
        isReload = true;
        //재장전 애니메이션 실행
        //animator.SetTrigger(hashReload);
        //재장전 사운드 발생
        //audio.PlayOneShot(reloadSfx, 1.0f);
        //재장전 시간만큼 대기하는 동안 제어권 양보
        yield return new WaitForSeconds (reloadTime);
        remainingBullet = maxBullet;
        isReload = false;
    }
}
