using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private const float SPEED = 3f;
    private static bool isLeftPressed;
    private static bool isRightPressed;


    [SerializeField]
    private Canvas uiCanvas;
    [SerializeField]
    private GameObject controlTarget;
    [SerializeField]
    private RectTransform joystickPad;
    private RectTransform joystick;

    private float joystickRadius;
    private int leftDirection;

    public static bool GetKeyLeft()
    {
        return isLeftPressed;
    }

    public static bool GetKeyRight()
    {
        return isRightPressed;
    }


    private void Awake()
    {
        isLeftPressed = false;
        isRightPressed = false;

        joystick = GetComponent<RectTransform>();
        joystickRadius = joystick.rect.width / 2;
    }

    public void OnPointerDown(PointerEventData ped)
    {
        MoveJoystickPad(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        joystickPad.anchoredPosition = Vector2.zero;
        isLeftPressed = false;
        isRightPressed = false;
    }

    public void OnDrag(PointerEventData ped)
    {
        MoveJoystickPad(ped);
    }

    private void MoveJoystickPad(PointerEventData ped)
    {
        Vector2 inputVec = ped.position / uiCanvas.scaleFactor - joystick.anchoredPosition;

        inputVec = inputVec.magnitude > joystickRadius ? inputVec * (joystickRadius / inputVec.magnitude) : inputVec;

        joystickPad.anchoredPosition = inputVec;

        if (joystickPad.anchoredPosition.x < 0)
        {
            //Left
            isLeftPressed = true;
            isRightPressed = false;
        }
        else if (joystickPad.anchoredPosition.x > 0)
        {
            //Right
            isLeftPressed = false;
            isRightPressed = true;
        }
    }

    //public void OnDrawGizmos(PointerEventData ped)
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(joystick.anchoredPosition / uiCanvas.scaleFactor, (Vector2)Input.mousePosition);
    //}
}