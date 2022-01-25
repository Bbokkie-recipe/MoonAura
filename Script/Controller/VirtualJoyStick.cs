using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VirtualJoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [Header("JoyStick")]
    [SerializeField] private RectTransform pad;
    [SerializeField] private RectTransform stick;
    
    [Header("PlayerController")]
    [SerializeField] CharController playerController;

    [Header("JoyStickRange")]
    [SerializeField, Range(10, 180)]
    private float stickRange;
    [SerializeField]
    public RectTransform padRangeRectTr;
    #region Input for Player
    private Vector3 inputDir;
    private bool isInput = false;
    #endregion

    public enum JoystickType { Move, Rotate}
    public JoystickType joystickType;

    # region JoyStickOriginPos
    private Vector2 pivotPos;
    private Vector2 anchoredPos;
    private Vector2 sizeDeltaPos;
    #endregion


    public void Start()
    {
        anchoredPos = pad.anchoredPosition;
        sizeDeltaPos = pad.sizeDelta;
        pivotPos = pad.pivot;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(padRangeRectTr, eventData.position)) pad.position = eventData.position;
        ControlStick(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlStick(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        /*return pad origin pos*/
        pad.anchoredPosition = anchoredPos;
        pad.sizeDelta = sizeDeltaPos;
        pad.pivot = pivotPos;
        /*return stick origin pos*/
        stick.anchoredPosition = Vector3.zero;

        isInput = false;
        switch (joystickType)
        {
            case JoystickType.Move:
                playerController.Move(Vector3.zero);
                break;
            case JoystickType.Rotate:
                playerController.LookAround(Vector3.zero);
                break;
        }
    }

    private void ControlStick(PointerEventData eventData)
    {
        var inputPos = eventData.position - pad.anchoredPosition;
        var inputVector = inputPos.sqrMagnitude < stickRange * stickRange ? inputPos : inputPos.normalized * stickRange;
        stick.anchoredPosition = inputVector;
        inputDir = inputVector / stickRange;
    }

    private void InputControlVec()
    {
        switch (joystickType)
        {
            case JoystickType.Move:
                playerController.Move(inputDir);
                break;
            case JoystickType.Rotate:
                playerController.LookAround(inputDir);
                break;
        }
    }

    void FixedUpdate()
    {
        if (isInput&& Input.touchCount != 2)
        {
            InputControlVec();
        }
    }

}
