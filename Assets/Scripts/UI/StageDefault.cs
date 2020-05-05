using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageDefault : MonoBehaviour
{
    private Image[] Images;
    private Button[] button;

    private void Awake()
    {
        Images = GetComponentsInChildren<Image>();
        button = GetComponentsInChildren<Button>();
        for (int i = 0; i < Images.Length; i++)
        {
            Images[i].color = Color.gray;
            button[i].interactable = false;
        }
    }
    private void Start()
    {
        for (int i = 0; i < DatabaseManager.instance.currentPlayerClearStage; i++)
        {
            Images[i].color = Color.white;
            button[i].interactable = true;
        }
    }
}
