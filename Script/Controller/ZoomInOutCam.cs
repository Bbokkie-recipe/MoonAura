using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOutCam : MonoBehaviour
{
    [Header("Camera Zoom Mode")]
    [SerializeField] private Camera MainCam;
    [SerializeField] private Transform camTr;
    [SerializeField] public Transform camtargetPlayer;
    [SerializeField] private float zoomSpeed;

    public float moveSensitivityX = 1.0f;
    public float moveSensitivityY = 1.0f;
    public bool updateZoomSensitivity = true;
    Vector3 PositionInfo;
    Vector3 OriginCamPos;
    public float zMin;
    public float zMax;
    public float yMin;
    public float yMax;
    public float xMin;
    public float xMax;
    Vector3 velocity = Vector3.zero;

    private void Start()
    {
        OriginCamPos = camTr.localPosition;
    }
    void FixedUpdate()
    {
        //카메라 이상 상태 -> 원 위치로
        CameraSmoothDamp();

        PositionInfo = (camTr.position - camtargetPlayer.position).normalized;

        MainCam.orthographicSize = (Screen.height / (Screen.width / 16f)) / 9f;

        Zoom();
        float z = Mathf.Clamp(camTr.localPosition.z, zMin, zMax);
        camTr.localPosition = new Vector3((camTr.localPosition).x, (camTr.localPosition).y, z);
    }

    private void CameraSmoothDamp()
    {
        if (camTr.localPosition.z < zMin || camTr.localPosition.z > zMax || camTr.localPosition.y < yMin || camTr.localPosition.y > yMax || camTr.localPosition.x < xMin || camTr.localPosition.x > xMax)
        {
            camTr.localPosition = Vector3.SmoothDamp(camTr.localPosition, OriginCamPos, ref velocity, 0.5f);
        }
    }

    private void Zoom()
    {
        if (Input.touchCount != 2) return;
        if (Input.touchCount == 2)
        {
            Touch touchZreo = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZreo.position - touchZreo.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float curMagnitude = (touchZreo.position - touchOne.position).magnitude;

            float difference = (touchZreo.deltaPosition - touchOne.deltaPosition).magnitude * zoomSpeed;

            if (prevMagnitude> curMagnitude)//zoom out
            {
                camTr.position = camTr.position - -(PositionInfo * difference);
            }
            else if(prevMagnitude < curMagnitude)//zoom in 
            {
                camTr.position = camTr.position + -(PositionInfo * difference);
            }
        }
    }

}
