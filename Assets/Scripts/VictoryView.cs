using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryView : MonoBehaviour
{
    public Text goldText;
    public GameObject[] stars;

    private void Awake()
    {
        SoundManager.instance.StopBgm();
    }
    public void GetInfo(int gold, int starCount = 0, int clearStage = 1)
    {
        goldText.text = gold.ToString() + " + " + clearStage * 10;
        for(int i = 0; i < starCount;i++)
        {
            if(i == 3)
            {
                break;
            }
            stars[i].gameObject.SetActive(true);
        }
    }

    public void Onclick()
    {
        SceneManager.LoadScene("StartScene");
    }
}
