using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick_WorldScreen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    public Canvas UICanvas;
    public RectTransform joystickBG;
    public RectTransform joystickPad;

    public GameObject player;

    //private float padWidthThreshold;
    //private float padHeightThreshold;

    private float radius;

    //private bool isPressed = false;
    private float UICanvasZPos;

    // Start is called before the first frame update
    void Start()
    {
        UICanvasZPos = UICanvas.transform.position.z;

        //padWidthThreshold = joystickBG.rect.width - joystickPad.rect.width;
        //padHeightThreshold = joystickBG.rect.height - joystickPad.rect.height;

        radius = joystickBG.rect.width - joystickPad.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(joystickPad.localPosition.x) < float.Epsilon)
        {
            return;
        }
        else if (joystickPad.localPosition.x < 0)
        {
            //Left
            player.transform.Translate(-10 * Time.deltaTime, 0, 0);
        }
        else
        {
            //Right
            player.transform.Translate(10 * Time.deltaTime, 0, 0);
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        //Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(ped.position);
        //joystickPad.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, UICanvasZPos);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        joystickPad.anchoredPosition = Vector3.zero;
    }

    public void OnDrag(PointerEventData ped)
    {
        radius = joystickBG.rect.width / 2;

        //Vector2 diffVector = ped.position - (Vector2)Camera.main.WorldToScreenPoint(joystickBG.position);
        //float diff = Mathf.Abs(diffVector.magnitude);

        //Vector from pad origin, to mouse position
        //Vector2 diffVector = ped.position - (Vector2)Camera.main.WorldToScreenPoint(joystickBG.position);
        Vector2 diffVector = ped.position - (Vector2)joystickBG.anchoredPosition;
        float diff = diffVector.magnitude;

        //Debug.Log($"{ped.position}, {joystickPad.localPosition.magnitude}, [{currentWidthDiff}], {diffVector}");
        //if (diff > radius)
        //{
        //    newPos = diffVector.normalized;
        //}

        Debug.Log($"BGPos: {joystickBG.position}, diffVec{diffVector}");

        if (diff > radius)
        {
            //Debug.Log($"out, {diff} {radius}");
            //mouseWorldPos = new Vector2(radius / diff * mouseWorldPos.x, radius / diff * mouseWorldPos.y);
        }

        //Vector2 mousePos = diff > radius ? new Vector2(radius / diff * ped.position.x, radius / diff * ped.position.y) : ped.position;

        //Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        //joystickPad.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, UICanvasZPos);
        joystickPad.anchoredPosition = diffVector;//new Vector2(diffVector.x, diffVector.y);
    }

    public void OnEndDrag(PointerEventData ped)
    {

    }

    public void OnDrawGizmos()
    {
        //Gizmos.DrawLine(new Vector3(joystickBG.position.x, joystickBG.position.y, UICanvasZPos), 
        //    new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, UICanvasZPos));
        Gizmos.DrawLine(new Vector3(joystickBG.position.x, joystickBG.position.y, 0),
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, UICanvasZPos));
    }
}