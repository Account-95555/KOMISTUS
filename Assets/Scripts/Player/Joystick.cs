using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;

    public float joystickDist;

    public bool isDragging;

    public Vector2 joystickVector;

    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        joystickOriginalPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(joystickDist);
    }

    public void PointerDown()
    {
        joystick.transform.position = Input.mousePosition;
        joystickBG.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        isDragging = true;
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVector = (dragPos - joystickTouchPos).normalized;

        joystickDist = Vector2.Distance(dragPos, joystickTouchPos);
        

        if(joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVector * joystickDist;
        }
        else
        {
            joystick.transform.position = joystickTouchPos + joystickVector * joystickRadius;
        }
    }

    public void PointerUp()
    {
        isDragging = false;
        joystickVector = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
        joystickBG.transform.position = joystickOriginalPos;
    }


}