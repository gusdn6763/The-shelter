using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FaildPanel : MonoBehaviour
{
    public void OnClickRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LoadingScene");
    }
    public void OnClickQuit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
}
