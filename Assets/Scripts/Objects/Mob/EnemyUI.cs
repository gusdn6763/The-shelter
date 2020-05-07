using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    private Canvas uiCanvas;

    private GameObject hpBar;           //몹이 죽으면 삭제하기위해 저장하는 오브젝트
    public GameObject hpBarPrefab;
    private GameObject reloadBar;
    public GameObject reloadingPrefab;

    private Image hpBarImage;           //체력바
    private GameObject reloadBullet;    //총알 수만큼
    private GameObject[] bullets;

    public Vector3 hpBarOffset;
    public Vector3 reloadingOffset;

    private int bulletcount;

    void Start()
    {
        uiCanvas = GameObject.Find("Mob Canvas").GetComponent<Canvas>();
        SetHpBar();
        SetReloadingBar();
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

        reloadBullet = reloadBar.GetComponent<EnemyReloadBar>().bulletBar;

        bulletcount = GetComponent<FireCtrl>().maxBullet;

        bullets = new GameObject[bulletcount];

        for (int i =0; i<bulletcount;i++)
        {
            GameObject tmp = Instantiate(reloadBullet, reloadBar.transform);
            bullets[i] = tmp;
        }
        var _reloadBar = reloadBar.GetComponent<EnemyReloadBar>();
        _reloadBar.targetTr = this.gameObject.transform;
        _reloadBar.offset = reloadingOffset;
    }

    public void DamagedBar(float hp)
    {
        hpBarImage.fillAmount = hp;
    }

    public void ReduceReloadBar(int count)
    {
        reloadBar.GetComponent<HorizontalLayoutGroup>().enabled = false;
        bullets[count].SetActive(false);
    }

    public void Charged()
    {
        reloadBar.GetComponent<HorizontalLayoutGroup>().enabled = true;
        for (int i = 0;i <bullets.Length;i++)
        {
            bullets[i].SetActive(true);
        }
        reloadBar.GetComponent<HorizontalLayoutGroup>().enabled = false;
    }

    public void DestoryUI()
    {
        Destroy(hpBar);
        Destroy(reloadBar);
    }
}
