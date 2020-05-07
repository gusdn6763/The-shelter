using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public PlayerManager player;
    public FireCtrl firectrl;

    public Text Bullet;
    public Text Hp;
    public Text Armor;
    public Text Money;

    public float hpMax;
    public int bulletMax;
    private void Awake()
    {
        player = GameManager.instance.player;
        firectrl = player.GetComponent<FireCtrl>();
        hpMax = player.HP;
        bulletMax = firectrl.maxBullet;
    }

    private void Update()
    {
        Bullet.text = firectrl.remainingBullet + " / " + bulletMax;
        Hp.text = player.currentHp + " / " + hpMax;
        Armor.text = player.Armor.ToString();
        Money.text = player.currentMoney.ToString();
    }

}
