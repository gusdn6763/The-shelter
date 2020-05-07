using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPopupView : MonoBehaviour
{
    public GameObject firstView;
    public GameObject secondView;

    public bool firstOrSecond = true;

    private void Awake()
    {
        secondView.SetActive(false);
    }
    public void ClickView()
    {
        firstOrSecond = !firstOrSecond;
        if(!firstOrSecond)
        {
            firstView.SetActive(true);
            secondView.SetActive(false);
        }
        else
        {
            firstView.SetActive(false);
            secondView.SetActive(true);
        }
    }
    public void onClickCancle()
    {
        Time.timeScale = 1f;
        Destroy(this.gameObject);
    }
}
