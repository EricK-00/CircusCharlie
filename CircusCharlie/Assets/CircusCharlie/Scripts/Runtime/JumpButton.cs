using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private static bool isJumpPressed;

    public static bool GetKeyJump()
    {
        return isJumpPressed;
    }

    private void Awake()
    {
        isJumpPressed = false;
    }

    public void OnPointerDown(PointerEventData ped)
    {
        isJumpPressed = true;
    }

    public void OnPointerUp(PointerEventData ped)
    {
        isJumpPressed = false;
    }
}