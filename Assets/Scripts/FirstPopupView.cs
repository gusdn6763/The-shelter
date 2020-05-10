using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPopupView : MonoBehaviour
{
    public GameObject[] popupView;

    public int numPopupView = 0;

    private void Awake()
    {
        SetActivePopup(0);
    }

    private void SetActivePopup(int num)
    {
        if (num < popupView.Length - 1 && num != 0)
        {
            popupView[num - 1].SetActive(false);
            popupView[num].SetActive(true);
            popupView[num + 1].SetActive(false);
        } else if (num == 0)
        {
            popupView[0].SetActive(true);
            popupView[1].SetActive(false);
        } else if (num == popupView.Length - 1)
        {
            popupView[num].SetActive(true);
            popupView[num - 1].SetActive(false);
        }
    }
    public void ClickNextView()
    {
        SetActivePopup(++numPopupView);
    }
    public void ClickBeforeView()
    {
        SetActivePopup(--numPopupView);
    }

    public void onClickCancle()
    {
        Time.timeScale = 1f;
        Destroy(this.gameObject);
    }
}
