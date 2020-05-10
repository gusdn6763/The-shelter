using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReloadBar : MonoBehaviour
{
    private Canvas canvas;
    private Camera uiCamera;
    private RectTransform rectParent;
    private RectTransform rectHp;
    public GameObject bulletBar;

    [HideInInspector] public Vector3 offset = Vector3.zero;
    [HideInInspector] public Transform targetTr;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        //월드 좌표를 스크린의 좌표로 변환
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);
        var localPos = Vector2.zero;
        //스크린 좌표를 RectTransform 기준의 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
        rectHp.localPosition = localPos;
    }
}