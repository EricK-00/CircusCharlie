using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private const float SPEED = 3f;

    [SerializeField]
    private Canvas uiCanvas;
    [SerializeField]
    private GameObject controlTarget;
    [SerializeField]
    private RectTransform joystickPad;
    private RectTransform joystick;

    [SerializeField]
    private bool isReversed = false;
    private float joystickRadius;
    private int leftDirection;

    void Awake()
    {
        joystick = GetComponent<RectTransform>();
        joystickRadius = joystick.rect.width / 2;
        leftDirection = isReversed ? 1 : -1;
    }

    void Update()
    {
        if (Mathf.Abs(joystickPad.anchoredPosition.x) < float.Epsilon)
        {
            return;
        }
        else if (joystickPad.anchoredPosition.x < 0)
        {
            //Left
            controlTarget.transform.Translate(leftDirection * SPEED * Time.deltaTime, 0, 0);
        }
        else
        {
            //Right
            controlTarget.transform.Translate(-leftDirection * SPEED * Time.deltaTime, 0, 0);
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        Vector2 inputVec = ped.position / uiCanvas.scaleFactor - joystick.anchoredPosition;

        inputVec = inputVec.magnitude > joystickRadius ? inputVec * (joystickRadius / inputVec.magnitude) : inputVec;

        joystickPad.anchoredPosition = inputVec;
    }

    public void OnPointerUp(PointerEventData ped)
    {
        joystickPad.anchoredPosition = Vector2.zero;
    }

    //public void OnDrawGizmos(PointerEventData ped)
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(joystick.anchoredPosition / uiCanvas.scaleFactor, (Vector2)Input.mousePosition);
    //}

    public void OnDrag(PointerEventData ped)
    {
        Vector2 inputVec = ped.position / uiCanvas.scaleFactor - joystick.anchoredPosition;

        inputVec = inputVec.magnitude > joystickRadius ? inputVec * (joystickRadius / inputVec.magnitude) : inputVec;

        joystickPad.anchoredPosition = inputVec;
    }
}