using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour
{
    public GameObject joystickPanel;
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
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(joystickDist);
    }

    public void PointerDown()
    {
        int i = Input.touches.Length; ;

        if (i == 1)
        {
            joystick.transform.position = Input.mousePosition;
            joystickBG.transform.position = Input.mousePosition;
            joystickTouchPos = Input.mousePosition;
        }
        else
        {
            int id = SortArray();
            joystick.transform.position = Input.touches[id].position;
            joystickBG.transform.position = Input.touches[id].position;
            joystickTouchPos = Input.touches[id].position;
        }

    }
    //SORTARRAYCODE by Rafcon on YT, prevents joystick from going to middle of screen
    private int SortArray()
    {
        Touch[] inputlist = Input.touches;
        float[] listpositioninputX = new float[inputlist.Length];
        int id = 0;
        for (int i = 0; i < inputlist.Length; i++)
        {
            listpositioninputX[i] = inputlist[i].position.x;
        }
        System.Comparison<float> compare = new System.Comparison<float>((num1, num2) => num1.CompareTo(num2));
        System.Array.Sort<float>(listpositioninputX, compare);

        for (int i = 0; i < inputlist.Length; i++)
        {
            if (listpositioninputX[0] == inputlist[i].position.x)
            {
                return id = inputlist[i].fingerId;
            }
        }
        return id;
    }
    
    public void Drag(BaseEventData baseEventData)
    {
        isDragging = true;
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVector = (dragPos - joystickTouchPos).normalized;

        joystickDist = Vector2.Distance(dragPos, joystickTouchPos);


        if (joystickDist < joystickRadius)
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
