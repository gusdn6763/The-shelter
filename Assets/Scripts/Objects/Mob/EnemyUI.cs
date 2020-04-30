using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    //부모가 될  오브젝트
    private Canvas uiCanvas;

    private GameObject hpBar;
    public GameObject hpBarPrefab;
    private GameObject reloadBar;
    public GameObject reloadingPrefab;

    private Image hpBarImage;

    public Vector3 hpBarOffset;

    void Start()
    {
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        SetHpBar();
    }

    void SetHpBar()
    {
        hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var _hpBar = hpBar.GetComponent<EnemyHpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;
    }

    void SetReloadingBar()
    {
        reloadBar = Instantiate<GameObject>(reloadingPrefab, uiCanvas.transform);
        hpBarImage = reloadBar.GetComponentsInChildren<Image>()[1];

        var _hpBar = hpBar.GetComponent<EnemyHpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;
    }

    public void DamagedBar(float hp)
    {
        hpBarImage.fillAmount = hp;

        if (hp <= 0.0f)
        {
            //적 캐릭터가 사망한 이후 생명 게이지를 투명 처리
            hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
            //적 캐릭터의 사망 횟수를 누적시키는 함수 호출
            // GameManager.instance.IncKillCount();
            Destroy(hpBar);
        }
    }

    public void ReduceReloadBar(float hp)
    {
        hpBarImage.fillAmount = hp;

        if (hp <= 0.0f)
        {

        }
    }

}
