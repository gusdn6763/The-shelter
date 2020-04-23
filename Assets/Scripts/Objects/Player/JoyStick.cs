using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler,IBeginDragHandler
{
    [SerializeField] GameObject joystickActiveRangeObj;
    [SerializeField] GameObject joystickObj;

    private RectTransform joystickActiveRange;
    private RectTransform joystick;

    public PlayerManager player;

    public Vector2 m_VecJoystickValue { get; private set; }
    public Vector3 m_VecJoyRotValue { get; private set; }

    public float moveSpeed = 10.0f;
    public float rotSpeed = 80.0f;

    private Vector3 StickFirstPos;
    private float m_fRadius;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        joystickActiveRange = joystickActiveRangeObj.GetComponent<RectTransform>();
        joystick = joystickObj.GetComponent<RectTransform>();
        m_fRadius = joystickActiveRange.rect.width * 0.5f;
        joystickActiveRangeObj.SetActive(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        joystickActiveRangeObj.transform.position = eventData.position;
        joystickObj.transform.position = eventData.position;
        joystickActiveRangeObj.SetActive(true);
        StickFirstPos = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        player.playerStatus = PlayerManager.CharacterStatus.MOVE;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 Pos = eventData.position;
        Vector3 JoyVec = (Pos - StickFirstPos).normalized;
        JoyStickMove(eventData);
        player.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(JoyVec.y, JoyVec.x) * Mathf.Rad2Deg + 90f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        joystickActiveRangeObj.SetActive(false);
        player.playerStatus = PlayerManager.CharacterStatus.IDLE;
    }

    void JoyStickMove(PointerEventData eventData)
    {
        m_VecJoystickValue = eventData.position - (Vector2)joystickActiveRange.position;
        m_VecJoystickValue = Vector2.ClampMagnitude(m_VecJoystickValue, m_fRadius);
        joystick.localPosition = m_VecJoystickValue;
        m_VecJoyRotValue = new Vector3(joystick.localPosition.x, 0f, joystick.localPosition.y);
    }
}